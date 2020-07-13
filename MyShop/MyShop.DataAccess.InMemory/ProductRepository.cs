using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShope.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products==null)
            {
                products = new List<Product>();
            }

        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public Product Update(String id)
        {
            Product productToUpdate = products.Find(p => p.Id == id);
            if(productToUpdate != null)
            {
                return productToUpdate;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }

        //Find
        public Product Find(String id)
        {
            Product product = products.Find(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }
        
        //List of Products
        public IQueryable Collection()
        {
            return products.AsQueryable();
        }

        //Delete
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete!= null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
