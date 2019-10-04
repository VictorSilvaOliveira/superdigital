using Superdigital.DataBase;
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
    public class AccountServiceTest
    {
        [Fact]
        public void AccountService_BlockMoney_Succesfull()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = serviceId;
                    return operation;
                });

            var transactionId = AccountService.BlockAmmount(accountId, ammount);

            Assert.Equal(serviceId, transactionId);

            accountDataBase.Verify(a => a.GetById(accountId), Moq.Times.Once);
            accountDataBase.Verify(a => a.Update(Moq.It.Is<AccountDetail>(ad => ad.Balance.Total == 0)), Moq.Times.Once);
            operationDataBase.Verify(a => a.Create(Moq.It.IsAny<Operation>()), Moq.Times.Once);
        }

        [Fact]
        public void AccountService_BlockMoney_AccountNotFoundException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
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
            var ammount = new Models.Money()
            {
                Total = 1000.01,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = serviceId;
                    return operation;
                });

            Assert.Throws<AccountNotFoundException>(() => AccountService.BlockAmmount(Guid.Empty, ammount));


            accountDataBase.Verify(a => a.GetById(Guid.Empty), Moq.Times.Once);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Create(Moq.It.IsAny<Operation>()), Moq.Times.Never);

        }

        [Fact]
        public void AccountService_BlockMoney_NotHaveSameCurrencyException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
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
                    Currency = "USD"
                }
            };
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = serviceId;
                    return operation;
                });

            Assert.Throws<NotHaveSameCurrencyException>(() => AccountService.BlockAmmount(accountId, ammount));


            accountDataBase.Verify(a => a.GetById(accountId), Moq.Times.Once);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Create(Moq.It.IsAny<Operation>()), Moq.Times.Never);

        }

        [Fact]
        public void AccountService_BlockMoney_NotEnoughMoneyException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
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
            var ammount = new Models.Money()
            {
                Total = 1000.01,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = serviceId;
                    return operation;
                });

            Assert.Throws<NotEnoughMoneyException>(() => AccountService.BlockAmmount(accountId, ammount));


            accountDataBase.Verify(a => a.GetById(accountId), Moq.Times.Once);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Create(Moq.It.IsAny<Operation>()), Moq.Times.Never);

        }

        [Fact]
        public void AccountService_ConfirmInOperation_Succesfull()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = transactionId;
                    return operation;
                });


            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.In,
                        Status = OperationStatus.Waiting,
                        TransactionId = id
                    };
                });

            var processedOperation = AccountService.ConfirmOperation(transactionId);

            Assert.Equal(transactionId, processedOperation.TransactionId);
            Assert.Equal(OperationStatus.Confirmed, processedOperation.Status);

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(accountId), Moq.Times.Once);
            accountDataBase.Verify(a => a.Update(Moq.It.Is<AccountDetail>(ad => ad.Balance.Total == 2000)), Moq.Times.Once);
            operationDataBase.Verify(a => a.Update(Moq.It.Is<Operation>(o => o.Status == OperationStatus.Confirmed)), Moq.Times.Once);
        }

        [Fact]
        public void AccountService_ConfirmOutOperation_Succesfull()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = transactionId;
                    return operation;
                });


            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.Out,
                        Status = OperationStatus.Waiting,
                        TransactionId = id
                    };
                });

            var processedOperation = AccountService.ConfirmOperation(transactionId);

            Assert.Equal(transactionId, processedOperation.TransactionId);
            Assert.Equal(OperationStatus.Confirmed, processedOperation.Status);

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Update(Moq.It.Is<Operation>(o => o.Status == OperationStatus.Confirmed)), Moq.Times.Once);
        }

        [Fact]
        public void AccountService_ConfirmInOperation_OperationAlreadyCommitedException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.In,
                        Status = OperationStatus.Reverted,
                        TransactionId = id
                    };
                });

            Assert.Throws<OperationAlreadyCommitedException>(() => AccountService.ConfirmOperation(transactionId));

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Update(Moq.It.IsAny<Operation>()), Moq.Times.Never);
        }

        [Fact]
        public void AccountService_ConfirmOutOperation_OperationAlreadyCommitedException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.Out,
                        Status = OperationStatus.Reverted,
                        TransactionId = id
                    };
                });

            Assert.Throws<OperationAlreadyCommitedException>(() => AccountService.ConfirmOperation(transactionId));

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Update(Moq.It.IsAny<Operation>()), Moq.Times.Never);
        }

        [Fact]
        public void AccountService_RevertOutOperation_Succesfull()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.Out &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = transactionId;
                    return operation;
                });


            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.Out,
                        Status = OperationStatus.Waiting,
                        TransactionId = id
                    };
                });

            var processedOperation = AccountService.RevertOperation(transactionId);

            Assert.Equal(transactionId, processedOperation.TransactionId);
            Assert.Equal(OperationStatus.Reverted, processedOperation.Status);

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(accountId), Moq.Times.Once);
            accountDataBase.Verify(a => a.Update(Moq.It.Is<AccountDetail>(ad => ad.Balance.Total == 2000)), Moq.Times.Once);
            operationDataBase.Verify(a => a.Update(Moq.It.Is<Operation>(o => o.Status == OperationStatus.Reverted)), Moq.Times.Once);
        }

        [Fact]
        public void AccountService_RevertInOperation_Succesfull()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.In &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == Guid.Empty
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = transactionId;
                    return operation;
                });


            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.In,
                        Status = OperationStatus.Waiting,
                        TransactionId = id
                    };
                });

            var processedOperation = AccountService.RevertOperation(transactionId);

            Assert.Equal(transactionId, processedOperation.TransactionId);
            Assert.Equal(OperationStatus.Reverted, processedOperation.Status);

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Update(Moq.It.Is<Operation>(o => o.Status == OperationStatus.Reverted)), Moq.Times.Once);
        }

        [Fact]
        public void AccountService_RevertOutOperation_OperationAlreadyCommitedException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.Out,
                        Status = OperationStatus.Confirmed,
                        TransactionId = id
                    };
                });

            Assert.Throws<OperationAlreadyCommitedException>(() => AccountService.RevertOperation(transactionId));

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Update(Moq.It.IsAny<Operation>()), Moq.Times.Never);
        }

        [Fact]
        public void AccountService_RevertInOperation_OperationAlreadyCommitedException()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(o => o.GetById(transactionId))
                .Returns<Guid>((id) =>
                {
                    return new Operation()
                    {
                        Id = id,
                        AccountId = accountId,
                        Ammount = new Models.Money()
                        {
                            Currency = ammount.Currency,
                            Total = ammount.Total
                        },
                        OperationType = OperationType.In,
                        Status = OperationStatus.Confirmed,
                        TransactionId = id
                    };
                });

            Assert.Throws<OperationAlreadyCommitedException>(() => AccountService.RevertOperation(transactionId));

            operationDataBase.Verify(o => o.GetById(transactionId), Moq.Times.Once);
            accountDataBase.Verify(a => a.GetById(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountDataBase.Verify(a => a.Update(Moq.It.IsAny<AccountDetail>()), Moq.Times.Never);
            operationDataBase.Verify(a => a.Update(Moq.It.IsAny<Operation>()), Moq.Times.Never);
        }


        [Fact]
        public void AccountService_SendMoney_Succesfull()
        {
            var accountDataBase = new Moq.Mock<IDataBase<Models.AccountDetail>>();
            var operationDataBase = new Moq.Mock<IDataBase<Operation>>();
            var AccountService = new AccountService(accountDataBase.Object, operationDataBase.Object);
            var transactionId = Guid.NewGuid();
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
            var ammount = new Models.Money()
            {
                Total = 1000,
                Currency = "BRL"
            };

            accountDataBase
                .Setup(a => a.GetById(accountId))
                .Returns(accountDetail);

            operationDataBase
                .Setup(a => a.Create(Moq.It.Is<Operation>(o =>
                    o.AccountId == accountId &&
                    o.Ammount.Currency == ammount.Currency &&
                    o.Ammount.Total == ammount.Total &&
                    o.OperationType == OperationType.In &&
                    o.Status == OperationStatus.Waiting &&
                    o.TransactionId == transactionId
                )))
                .Returns<Operation>((operation) =>
                {
                    operation.Id = Guid.NewGuid();
                    return operation;
                });

            AccountService.SendMoney(accountId, ammount, transactionId);

            operationDataBase.Verify(a => a.Create(Moq.It.Is<Operation>(o =>
                o.AccountId == accountId &&
                o.Ammount.Currency == ammount.Currency &&
                o.Ammount.Total == ammount.Total &&
                o.OperationType == OperationType.In &&
                o.Status == OperationStatus.Waiting &&
                o.TransactionId == transactionId
            )), Moq.Times.Once);
        }

    }
}