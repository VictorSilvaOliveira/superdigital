using Superdigital.DataBase;
using Superdigital.Handlers.Exceptions;
using Superdigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superdigital.Handlers.Account
{
    public class AccountService : IAccountService
    {
        private readonly IDataBase<AccountDetail> _accounts;
        private readonly IDataBase<Operation> _operations;

        public AccountService(IDataBase<AccountDetail> accounts, IDataBase<Operation> operations)
        {
            _accounts = accounts;
            _operations = operations;
        }

        public Guid BlockAmmount(Guid accountId, Money ammount)
        {
            var account = _accounts.GetById(accountId);
            if (account.Equals(default(AccountDetail)))
            {
                throw new AccountNotFoundException();
            }
            if (account.Balance.Currency != ammount.Currency)
            {
                throw new NotHaveSameCurrencyException();
            }

            if (account.Balance.Total < ammount.Total)
            {
                throw new NotEnoughMoneyException();
            }

            account.Balance = account.Balance - ammount;

            _accounts.Update(account);

            return CreateOperation(accountId, ammount, OperationType.Out);
        }

        public Operation ConfirmOperation(object operationId)
        {
            return CommitOperation(operationId, OperationStatus.Confirmed, OperationType.In, OperationStatus.Reverted);
        }

        public Models.AccountDetail FindAccount(Models.Account account)
        {
            var accounts = _accounts.Find(a => a.Account.IsSame(account)).ToList();
            if (accounts.Count == 0)
            {
                throw new AccountNotFoundException();
            }

            if (accounts.Count > 1)
            {
                throw new AccountDuplicatedException();
            }

            return accounts[0];
        }

        public List<Operation> FindOperations(Guid transactionId)
        {
            return _operations.Find(o => o.TransactionId == transactionId).ToList();
        }

        public Operation RevertOperation(object operationId)
        {
            return CommitOperation(operationId, OperationStatus.Reverted, OperationType.Out, OperationStatus.Confirmed);
        }

        public void SendMoney(Guid accountId, Money ammount, Guid transactionId)
        {
            CreateOperation(accountId, ammount, OperationType.In, transactionId);
        }

        private Guid CreateOperation(Guid accountId, Money ammount, OperationType operationType, Guid? transactionId = null)
        {
            Operation register = new Operation()
            {
                Ammount = ammount,
                OperationType = operationType,
                Status = OperationStatus.Waiting,
                AccountId = accountId
            };
            if (transactionId.HasValue)
            {
                register.TransactionId = transactionId.Value;
            }
            var operation = _operations.Create(register);
            operation.TransactionId = transactionId ?? (Guid)operation.Id;
            return operation.TransactionId;
        }

        private Operation CommitOperation(object operationId, OperationStatus commitStatus, OperationType operationTypeRevertBalance, params OperationStatus[] commitedStatus)
        {
            var operation = _operations.GetById(operationId);
            if (commitedStatus.Any(s => s.Equals(operation.Status)))
            {
                throw new OperationAlreadyCommitedException();
            }
            operation.Status = commitStatus;
            if (operation.OperationType == operationTypeRevertBalance)
            {
                var account = _accounts.GetById(operation.AccountId);
                account.Balance += operation.Ammount;
                _accounts.Update(account);
            }

            _operations.Update(operation);



            return operation;
        }
    }
}
