using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShope.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        InMemoryRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productsCategory = context.Collection().ToList();
            return View(productsCategory);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory p = context.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productToEdit = context.Find(id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    //context.Update(productCategory, id);
                    productToEdit.Category = productCategory.Category;
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }

        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToDelete = context.Find(id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    context.Delete(id);
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }


    }
}

