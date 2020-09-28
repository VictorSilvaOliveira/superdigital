using Superdigital.Models;
using System;

namespace Superdigital.Handlers.Account
{
    public interface IAccountHandler
    {
        Guid BlockAmmount(Models.Account account, Money ammount);

        void SendMoney(Models.Account account, Money ammount, Guid id);

        void ConfirmTransaction(Guid transactionId);

        void RevertTransaction(Guid transactionId);
    }
}