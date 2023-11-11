using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using BulkyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.admin.Controllers
{
    [Area("admin")]
/*    [Authorize(Roles = SD.Role_Admin)]
*/    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.Display_Order.ToString())
            {
                ModelState.AddModelError("name", "Name And Order Cannot Match");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Record Created Successfully!";
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryfromdb = _unitOfWork.category.Get(u => u.Category_Id == id);//works only with pk
            /*Category categoryfromdb1 = _db.Categories.FirstOrDefault(x=>x.Category_Id==id);
            Category categoryfromdb2 = _db.Categories.Where(x => x.Category_Id == id).FirstOrDefault()*/

            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Record Updated Successfully!";
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        public IActionResult Delete(int? id)
        {
            Category? categoryfromdb = _unitOfWork.category.Get(u => u.Category_Id == id); //works only with pk
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Remove(categoryfromdb);
                _unitOfWork.Save();
                TempData["success"] = "Record Deleted Successfully!";
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

    }
}