using System;
using Superdigital.Handlers.Exceptions;

namespace Superdigital.Handlers.Account
{
    public class AccountHandler : IAccountHandler
    {
        private readonly IAccountService _accountService;

        public AccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Guid BlockAmmount(Models.Account account, Models.Money ammount)
        {
            var accountDetail = _accountService.FindAccount(account);
            return _accountService.BlockAmmount((Guid)accountDetail.Id, ammount);
        }

        public void ConfirmTransaction(Guid transactionId)
        {
            CommitTransactions(transactionId, Models.OperationStatus.Confirmed, _accountService.ConfirmOperation);
        }

        public void RevertTransaction(Guid transactionId)
        {
            CommitTransactions(transactionId, Models.OperationStatus.Reverted, _accountService.RevertOperation);
        }

        private void CommitTransactions(Guid transactionId, Models.OperationStatus operationStatusExpected, Func<object, Models.Operation> commitAction)
        {
            var operations = _accountService.FindOperations(transactionId);
            operations.ValidateOperations();

            operations.ForEach(operation =>
            {
                var operationCommited = commitAction(operation.Id);
                if (operationCommited.Status != operationStatusExpected)
                {
                    throw new OperationNotRevertedException();
                }
            });
        }

        public void SendMoney(Models.Account account, Models.Money ammount, Guid transactionId)
        {
            var accountDetail = _accountService.FindAccount(account);
            _accountService.SendMoney((Guid)accountDetail.Id, ammount, transactionId);
        }
    }
}