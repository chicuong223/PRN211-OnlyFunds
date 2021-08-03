﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace OnlyFundsWeb.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository = null;
        private IPostCategoryMapRepository postCategoryMapRepository = null;

        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
            postCategoryMapRepository = new PostCategoryMapRepository();
        }
        // GET: CategoryController
        public ActionResult CategoryList()
        {
            IEnumerable<Category> categories= categoryRepository.GetCategories();
            return View(categories);
        }


        // GET: CategoryController/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: CategoryController/Create
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(CategoryList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(CategoryList));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: CategoryController/Delete/5
        public ActionResult DeleteCategory(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(CategoryList));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
