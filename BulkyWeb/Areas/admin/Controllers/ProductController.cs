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
*/    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.product.GetAll(includeProperties : "Category").ToList();
            
             return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            //ViewBag.CategoryList = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Category_Id.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id==0)
            {
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.product.Get(u=>u.Id==id);
                return View(productVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj , IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\product\");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        //delete existing image
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = @"\Images\product\" + fileName;
                }

                if (obj.Product.Id == 0)
                {
                    _unitOfWork.product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.product.Update(obj.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Record Created Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                ProductVM productVM = new()
                {
                    CategoryList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Category_Id.ToString()
                    }),
                    Product = new Product()
                };
                return View(productVM);
            }

        }
        

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

/*        [HttpDelete]
*/
        public IActionResult Delete(int? id)
        {
            Product? productfromdb = _unitOfWork.product.Get(u => u.Id == id); //works only with pk
            if (ModelState.IsValid)
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                    productfromdb.ImageUrl.Trim('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitOfWork.product.Remove(productfromdb);
                _unitOfWork.Save();
                return Json(new {success = true,message = "Deleted Succesfully"});
            }

            return View("Edit");
        }
        #endregion
    }
}