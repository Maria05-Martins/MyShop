using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if(items==null)
            {
                items = new List<T>();
            }
        }
        
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t,string id)
        {
            T ttoUpdate = items.Find(i => i.Id == id);
            if(ttoUpdate != null)
            {
                ttoUpdate = t;
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

        public T Find(string id)
        {
            T t = items.Find(i => i.Id == id);
            if(t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

        public IQueryable<T> Collection ()
        {
            return items.AsQueryable();
        }

        public void Delete(string id)
        {
            T tToDelete = items.Find(i => i.Id == id);

            if(tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

        public IEnumerable<ProductCategory> Find(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
