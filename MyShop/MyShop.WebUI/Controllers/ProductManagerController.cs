﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using MyShope.Core.Models;

namespace MyShope.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController(IRepository<Product> context, IRepository<ProductCategory> productCategories)
        {
            this.context = context;
            this.productCategories = productCategories;
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            Product productToEdit = context.Find(id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = productToEdit; //new Product();
                viewModel.ProductCategories = (IEnumerable<ProductCategory>)productCategories.Collection().Select(p=>p.Category==productToEdit.Category);// productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product,string id)
        {
            Product product1ToEdit = context.Find(id);
            if(product1ToEdit==null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    //context.Update(product, id);
                    product1ToEdit.Name = product.Name;
                    product1ToEdit.Description = product.Description;
                    product1ToEdit.Image = product.Image;
                    product1ToEdit.Category = product.Category;                
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string id)
        {
            Product product = context.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }

        }

            
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Product product,string id)
        {
            Product productToDelete = context.Find(id);
            if(productToDelete==null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
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