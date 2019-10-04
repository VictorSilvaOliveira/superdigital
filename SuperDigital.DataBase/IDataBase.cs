using System;
using System.Collections.Generic;

namespace Superdigital.DataBase
{
    public interface IDataBase<T>
    {
        T Create(T register);
        T GetById(object id);
        IEnumerable<T> Find(Predicate<T> predicate);
        void Update(T register);
    }
}