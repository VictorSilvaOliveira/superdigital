using Superdigital.Handlers.Account;
using Superdigital.Handlers.Exceptions;
using Superdigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Superdigital.Handlers.Test
{
    public class AccountHandlerTest
    {
        [Fact]
        public void AccountHandler_BlockMoney_Succesfull()
        {
            var accountService = new Moq.Mock<IAccountService>();
            var accountHandler = new AccountHandler(accountService.Object);
            var serviceId = Guid.NewGuid();
            Guid accountId = Guid.NewGuid();
            var origin = new Models.Account();
            var accountDetail = new Models.AccountDetail()
            {
                Id = accountId,
                Account = origin,
                Balance = new Money()
                {
                    Total = 1000,
                    Currency = "BRL"
                }
            };
            var ammount = new Models.Money();

            accountService
                .Setup(a => a.FindAccount(origin))
                .Returns(accountDetail);

            accountService
                .Setup(a => a.BlockAmmount(accountId, ammount))
                .Returns(serviceId);

            var transactionId = accountHandler.BlockAmmount(origin, ammount);

            Assert.Equal(serviceId, transactionId);

            accountService.Verify(a => a.FindAccount(origin), Moq.Times.Once);
            accountService.Verify(a => a.BlockAmmount(accountId, ammount), Moq.Times.Once);

        }

        [Fact]
        public void AccountHandler_ConfirmTransaction_Succesfull()
        {
            var accountService = new Moq.Mock<IAccountService>();
            var accountHandler = new AccountHandler(accountService.Object);
            var transactionId = Guid.NewGuid();

            var ammount = new Models.Money();
            var listOperation = new List<Operation>()
            {
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.Out,
                    Status = OperationStatus.Waiting
                },
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.In,
                    Status = OperationStatus.Waiting
                }
            };
            accountService
                .Setup(a => a.FindOperations(transactionId))
                .Returns(listOperation);

            accountService
                .Setup(a => a.ConfirmOperation(Moq.It.IsAny<Guid>()))
                .Returns<Guid>((operationId) =>
                {
                    var operation = listOperation.FirstOrDefault(o => (Guid)o.Id == operationId);
                    operation.Status = OperationStatus.Confirmed;
                    return operation;
                });

            accountHandler.ConfirmTransaction(transactionId);

            accountService.Verify(a => a.FindOperations(transactionId), Moq.Times.Once);
            accountService.Verify(a => a.ConfirmOperation((Guid)listOperation[0].Id), Moq.Times.Once);
            accountService.Verify(a => a.ConfirmOperation((Guid)listOperation[1].Id), Moq.Times.Once);

        }

        [Fact]
        public void AccountHandler_RevertTransaction_Succesfull()
        {
            var accountService = new Moq.Mock<IAccountService>();
            var accountHandler = new AccountHandler(accountService.Object);
            var transactionId = Guid.NewGuid();

            var ammount = new Models.Money();
            var listOperation = new List<Operation>()
            {
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.Out,
                    Status = OperationStatus.Waiting
                },
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.In,
                    Status = OperationStatus.Waiting
                }
            };
            accountService
                .Setup(a => a.FindOperations(transactionId))
                .Returns(listOperation);

            accountService
                .Setup(a => a.RevertOperation(Moq.It.IsAny<Guid>()))
                .Returns<Guid>((operationId) =>
                {
                    var operation = listOperation.FirstOrDefault(o => (Guid)o.Id == operationId);
                    operation.Status = OperationStatus.Reverted;
                    return operation;
                });

            accountHandler.RevertTransaction(transactionId);

            accountService.Verify(a => a.FindOperations(transactionId), Moq.Times.Once);
            accountService.Verify(a => a.RevertOperation((Guid)listOperation[0].Id), Moq.Times.Once);
            accountService.Verify(a => a.RevertOperation((Guid)listOperation[1].Id), Moq.Times.Once);

        }

        [Fact]
        public void AccountHandler_SendMoney_Succesfull()
        {
            var accountService = new Moq.Mock<IAccountService>();
            var accountHandler = new AccountHandler(accountService.Object);
            var transactionId = Guid.NewGuid();
            Guid accountId = Guid.NewGuid();
            var account = new Models.Account();
            var ammount = new Models.Money();
            var accountDetail = new Models.AccountDetail()
            {
                Id = accountId,
                Account = account,
                Balance = new Money()
                {
                    Total = 1000,
                    Currency = "BRL"
                }
            };
            accountService
                .Setup(a => a.FindAccount(account))
                .Returns(accountDetail);

            accountHandler.SendMoney(account, ammount, transactionId);

            accountService.Verify(a => a.FindAccount(account), Moq.Times.Once);
            accountService.Verify(a => a.SendMoney(accountId, ammount, transactionId), Moq.Times.Once);

        }

        [Fact]
        public void AccountHandler_CommitTransaction_InvalidNumberOfOperationsException()
        {
            var accountService = new Moq.Mock<IAccountService>();
            var accountHandler = new AccountHandler(accountService.Object);
            var transactionId = Guid.NewGuid();

            var ammount = new Models.Money();
            var listOperation = new List<Operation>()
            {
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.Out,
                    Status = OperationStatus.Waiting
                },
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.In,
                    Status = OperationStatus.Waiting
                },
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.In,
                    Status = OperationStatus.Waiting
                }
            };
            accountService
                .Setup(a => a.FindOperations(transactionId))
                .Returns(listOperation);

            Assert.Throws<InvalidNumberOfOperationsException>(() => accountHandler.ConfirmTransaction(transactionId));

            accountService.Verify(a => a.FindOperations(transactionId), Moq.Times.Once);
            accountService.Verify(a => a.ConfirmOperation(listOperation[0].Id), Moq.Times.Never);
            accountService.Verify(a => a.ConfirmOperation(listOperation[1].Id), Moq.Times.Never);
            //OperationsNotMatchException

        }

        [Fact]
        public void AccountHandler_CommitTransaction_OperationsNotMatchException()
        {
            var accountService = new Moq.Mock<IAccountService>();
            var accountHandler = new AccountHandler(accountService.Object);
            var transactionId = Guid.NewGuid();

            var ammount = new Models.Money();
            var listOperation = new List<Operation>()
            {
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.In,
                    Status = OperationStatus.Waiting
                },
                new Operation()
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    Ammount= ammount,
                    OperationType = OperationType.In,
                    Status = OperationStatus.Waiting
                }
            };
            accountService
                .Setup(a => a.FindOperations(transactionId))
                .Returns(listOperation);

            Assert.Throws<OperationsNotMatchException>(() => accountHandler.ConfirmTransaction(transactionId));

            accountService.Verify(a => a.FindOperations(transactionId), Moq.Times.Once);
            accountService.Verify(a => a.ConfirmOperation(listOperation[0].Id), Moq.Times.Never);
            accountService.Verify(a => a.ConfirmOperation(listOperation[1].Id), Moq.Times.Never);
        }
    }
}
