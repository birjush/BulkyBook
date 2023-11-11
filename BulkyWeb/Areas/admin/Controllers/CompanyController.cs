using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using BulkyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.ObjectPool;
using NuGet.ProjectModel;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace BulkyWeb.Areas.admin.Controllers
{
    [Area("admin")]
/*    [Authorize(Roles = SD.Role_Admin)]
*/    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<CompanyTable> objCompanyList = _unitOfWork.company.GetAll().ToList();
            
             return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            //ViewBag.CategoryList = CategoryList;
            if (id == null || id==0)
            {
                return View(new CompanyTable());
            }
            else
            {
                //update
                CompanyTable companyobj = _unitOfWork.company.Get(u=>u.Id==id);
                return View(companyobj);
            }

        }

        [HttpPost]
        public IActionResult Upsert(CompanyTable obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.company.Add(obj);
                }
                else
                {
                    _unitOfWork.company.Update(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Created Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }
        

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CompanyTable> objCompanyList = _unitOfWork.company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]

        public IActionResult Delete(int? id)
        {
            CompanyTable? companyfromdb = _unitOfWork.company.Get(u => u.Id == id); //works only with pk
            if (ModelState.IsValid)
            {
                _unitOfWork.company.Remove(companyfromdb);
                _unitOfWork.Save();
                return Json(new {success = true,message = "Deleted Succesfully"});
            }

            return View("Edit");
        }
        #endregion
    }
}