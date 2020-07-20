using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string id);
        IEnumerable<ProductCategory> Find(Func<object, bool> p);
        T Find(string id);
        void Insert(T t);
        void Update(T t, string id);
    }
}