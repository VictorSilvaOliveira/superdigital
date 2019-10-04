using Superdigital.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superdigital.FakeDatabases
{
    public abstract class FakeDataBase<T> : IDataBase<T> where T : IRegister
    {
        private Dictionary<object, T> _database = new Dictionary<object, T>();

        public T Create(T register)
        {
            register.Id = Guid.NewGuid();
            _database[register.Id] = register;
            return register;
        }

        public IEnumerable<T> Find(Predicate<T> predicate)
        {
            return _database.Values.Where(r => predicate(r));
        }

        public T GetById(object id)
        {
            return _database[id];
        }

        public void Update(T register)
        {
            _database[register.Id] = register;

        }
    }
}