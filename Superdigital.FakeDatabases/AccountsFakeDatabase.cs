using Superdigital.DataBase;
using Superdigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superdigital.FakeDatabases
{
    public class AccountsFakeDatabase : FakeDataBase<AccountDetail>
    {
        public AccountsFakeDatabase()
        {
            Create(new AccountDetail()
            {
                Account = new Account()
                {
                    AccountNumber = new AccountNumber()
                    {
                        Number = "1234",
                        Validador = "5"
                    },
                    Agency = new Agency()
                    {
                        Number = "5678",
                        Validador = "9",
                    },
                    Bank = new Bank()
                    {
                        Id = "123",
                        Name = "Bank"
                    },
                    Owner = new Person()
                    {
                        Id = new PersonalId()
                        {
                            Number = "123456789",
                            Validador = "11",
                        },
                        Name = "Joao",
                        SurName = "Silva"
                    }
                },
                Balance = new Money()
                {
                    Currency = "BRL",
                    Total = 1000
                },
                Id = Guid.NewGuid()
            });

            Create(new AccountDetail()
            {
                Account = new Account()
                {
                    AccountNumber = new AccountNumber()
                    {
                        Number = "2345",
                        Validador = "6"
                    },
                    Agency = new Agency()
                    {
                        Number = "6789",
                        Validador = "0",
                    },
                    Bank = new Bank()
                    {
                        Id = "123",
                        Name = "Bank"
                    },
                    Owner = new Person()
                    {
                        Id = new PersonalId()
                        {
                            Number = "234567890",
                            Validador = "22",
                        },
                        Name = "Jonas",
                        SurName = "Silva"
                    }
                },
                Balance = new Money()
                {
                    Currency = "BRL",
                    Total = 1000
                },
                Id = Guid.NewGuid()
            });
        }
    }
}
