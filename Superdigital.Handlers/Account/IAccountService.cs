using Superdigital.Models;
using System;
using System.Collections.Generic;

namespace Superdigital.Handlers.Account
{
    public interface IAccountService
    {
        Models.AccountDetail FindAccount(Models.Account account);
        Guid BlockAmmount(Guid accountId, Money ammount);
        void SendMoney(Guid accountId, Money ammount, Guid transactionId);
        List<Operation> FindOperations(Guid transactionId);
        Operation RevertOperation(object operationId);
        Operation ConfirmOperation(object operationId);
    }
}