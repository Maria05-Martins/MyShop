using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCategory = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productsCategory = cache["productsCategory"] as List<ProductCategory>;
            if (productsCategory == null)
            {
                productsCategory = new List<ProductCategory>();
            }

        }

        public void Commit()
        {
            cache["productsCategory"] = productsCategory;
        }

        public void Insert(ProductCategory p)
        {
            productsCategory.Add(p);
        }

        public ProductCategory Update(ProductCategory p, String id)
        {
            ProductCategory productCategoryToUpdate = productsCategory.Find(productCategory => productCategory.Id == id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate.Id = p.Id;
                productCategoryToUpdate.Category = p.Category;
                return p;
            }
            else
            {
                throw new Exception("Product Category not Found");
            }
        }

        //Find
        public ProductCategory Find(String id)
        {
            ProductCategory productCategory = productsCategory.Find(p => p.Id == id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not Found");
            }
        }

        //List of Products
        public IQueryable<ProductCategory> Collection()
        {
            return productsCategory.AsQueryable();
        }

        //Delete
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productsCategory.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productsCategory.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}

