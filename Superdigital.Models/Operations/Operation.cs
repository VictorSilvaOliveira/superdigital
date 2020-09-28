using Superdigital.DataBase;
using System;

namespace Superdigital.Models
{
    public struct Operation : IRegister
    {
        public object Id { get; set; }
        public Guid TransactionId { get; set; }
        public Money Ammount { get; set; }
        public OperationType OperationType { get; set; }
        public OperationStatus Status { get; set; }
        public Guid AccountId { get; set; }
    }
}