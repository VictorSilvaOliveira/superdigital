using Superdigital.Handlers.Account;
using Superdigital.Handlers.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Superdigital.Handlers.Test
{
    public class TransferHandlerTest
    {
        [Fact]
        public void TransferHandler_Execute_Succesfull()
        {
            var accountHandler = new Moq.Mock<IAccountHandler>();

            var ammount = new Models.Money();
            var origin = new Models.Account();
            var destiny = new Models.Account();
            var transactionId = Guid.NewGuid();

            var transaction = new Models.Transfer()
            {
                Ammount = ammount,
                Origin = origin,
                Destiny = destiny
            };

            accountHandler
                .Setup(a => a.BlockAmmount(origin, ammount))
                .Returns(transactionId);

            var transferHandler = new TransferHandler(accountHandler.Object);
            var proccessedTransactionId = transferHandler.Execute(transaction);

            Assert.Equal(transactionId, proccessedTransactionId);

            accountHandler.Verify(a => a.BlockAmmount(origin, ammount), Moq.Times.Once);
            accountHandler.Verify(a => a.SendMoney(destiny, ammount, transactionId), Moq.Times.Once);
            accountHandler.Verify(a => a.ConfirmTransaction(transactionId), Moq.Times.Once);
            accountHandler.Verify(a => a.RevertTransaction(Moq.It.IsAny<Guid>()), Moq.Times.Never);
        }

        [Fact]
        public void TransferHandler_Execute_NotEnoughMoneyException()
        {
            var accountHandler = new Moq.Mock<IAccountHandler>();

            var ammount = new Models.Money();
            var origin = new Models.Account();

            var transaction = new Models.Transfer()
            {
                Ammount = ammount,
                Origin = origin,
                Destiny = new Models.Account()
            };

            accountHandler
                .Setup(a => a.BlockAmmount(origin, ammount))
                .Throws(new NotEnoughMoneyException());

            var transferHandler = new TransferHandler(accountHandler.Object);
            Action throwException = () => transferHandler.Execute(transaction);

            Assert.Throws<NotEnoughMoneyException>(throwException);

            accountHandler.Verify(a => a.BlockAmmount(origin, ammount), Moq.Times.Once);
            accountHandler.Verify(a => a.SendMoney(Moq.It.IsAny<Models.Account>(), Moq.It.IsAny<Models.Money>(), Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountHandler.Verify(a => a.ConfirmTransaction(Moq.It.IsAny<Guid>()), Moq.Times.Never);
            accountHandler.Verify(a => a.RevertTransaction(Moq.It.IsAny<Guid>()), Moq.Times.Never);
        }

        [Fact]
        public void TransferHandler_Execute_AccountNotFoundException()
        {
            var accountHandler = new Moq.Mock<IAccountHandler>();

            var ammount = new Models.Money();
            var origin = new Models.Account();
            var destiny = new Models.Account();
            var transactionId = Guid.NewGuid();

            var transaction = new Models.Transfer()
            {
                Ammount = ammount,
                Origin = origin,
                Destiny = destiny
            };

            accountHandler
                .Setup(a => a.BlockAmmount(origin, ammount))
                .Returns(transactionId);

            accountHandler
                .Setup(a => a.SendMoney(destiny, ammount, transactionId))
                .Throws(new AccountNotFoundException());

            var transferHandler = new TransferHandler(accountHandler.Object);
            Action throwException = () => transferHandler.Execute(transaction);

            Assert.Throws<AccountNotFoundException>(throwException);

            accountHandler.Verify(a => a.BlockAmmount(origin, ammount), Moq.Times.Once);
            accountHandler.Verify(a => a.SendMoney(destiny, ammount, transactionId), Moq.Times.Once);
            accountHandler.Verify(a => a.ConfirmTransaction(transactionId), Moq.Times.Never);
            accountHandler.Verify(a => a.RevertTransaction(transactionId), Moq.Times.Once);
        }


        [Fact]
        public void TransferHandler_Execute_TimeOutOperationException()
        {
            var accountHandler = new Moq.Mock<IAccountHandler>();

            var ammount = new Models.Money();
            var origin = new Models.Account();
            var destiny = new Models.Account();
            var transactionId = Guid.NewGuid();

            var transaction = new Models.Transfer()
            {
                Ammount = ammount,
                Origin = origin,
                Destiny = destiny
            };

            accountHandler
                .Setup(a => a.BlockAmmount(origin, ammount))
                .Returns(transactionId);

            accountHandler
                .Setup(a => a.ConfirmTransaction(transactionId))
                .Throws(new TimeOutOperationException());

            var transferHandler = new TransferHandler(accountHandler.Object);
            Action throwException = () => transferHandler.Execute(transaction);

            Assert.Throws<TimeOutOperationException>(throwException);

            accountHandler.Verify(a => a.BlockAmmount(origin, ammount), Moq.Times.Once);
            accountHandler.Verify(a => a.SendMoney(destiny, ammount, transactionId), Moq.Times.Once);
            accountHandler.Verify(a => a.ConfirmTransaction(transactionId), Moq.Times.Once);
            accountHandler.Verify(a => a.RevertTransaction(transactionId), Moq.Times.Once);
        }


        
    }
}
