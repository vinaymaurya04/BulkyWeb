﻿using BulkyWeb.DAL.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult CreateCategoryList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategoryList(Category obj)
        {
            //Custom Validation
            if (obj.CategoryName == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "The Display order cannot be same as Category Name.");
            }
            //Server Validation
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? CategoryID)
        {
            if (CategoryID == 0 || CategoryID == null)
            {
                return NotFound();
            }
            Category categoryfromDb = _db.Categories.Find(CategoryID);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            //Custom Validation
            if (obj.CategoryName == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "The Display order cannot be same as Category Name.");
            }
            //Server Validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int? CategoryID)
        {
            if (CategoryID == 0 || CategoryID == null)
            {
                return NotFound();
            }
            Category categoryfromDb = _db.Categories.Find(CategoryID);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? CategoryID)
        {
            Category? Obj = _db.Categories.Find(CategoryID);
            if (Obj==null)
            {
                return NotFound();
            }
            _db.Categories.Remove(Obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deletdd Successfully";
            return RedirectToAction("Index");
          
        }
    }
}
