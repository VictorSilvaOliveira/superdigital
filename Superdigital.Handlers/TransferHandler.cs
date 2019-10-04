﻿using System;
using Superdigital.Handlers.Account;
using Superdigital.Models;

namespace Superdigital.Handlers
{
    public class TransferHandler
    {
        private readonly IAccountHandler _accountHandler;

        public TransferHandler(IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        public Guid Execute(Transfer transaction)
        {
            var transactionId = _accountHandler.BlockAmmount(transaction.Origin, transaction.Ammount);
            try
            {
                _accountHandler.SendMoney(transaction.Destiny, transaction.Ammount, transactionId);
                _accountHandler.ConfirmTransaction(transactionId);
            }
            catch (Exception ex)
            {
                _accountHandler.RevertTransaction(transactionId);
                throw ex;
            }

            return transactionId;
        }
    }
}