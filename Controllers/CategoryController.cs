using CRUDApp.Data;
using CRUDApp.Models;
using CRUDApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ConnectDB _context;
        public CategoryController(ConnectDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllCategory()
        {
            var category = _context.Category.ToList();
            var jsonCategory = Json(category);
            return jsonCategory;
        }

        public ActionResult GetCategoryPartial(int? id)
        {
            var category = _context.Category.Find(id) ?? new Category();
            return PartialView("_CreateOrUpdateCategory", category);
        }


        public ActionResult CreateOrUpdateCategory(CategoryCreateDTO categoryCreateDTO)
        {
            if(ModelState.IsValid)
            {
                var category = new Category { 
                    Id = categoryCreateDTO.Id,
                    Name = categoryCreateDTO.Name, 
                    Status = categoryCreateDTO.Status
                };
                if(categoryCreateDTO.Id > 0)
                {
                    _context.Update(category);

                } else
                {
                    _context.Category.Add(category);
                }
                _context.SaveChanges();
                return Json(true);
            }
            return PartialView("_CreateOrUpdateCategory", new Category());
        }

        public ActionResult DeleteCategory(int id)
        {
            //_context.Remove(id);
            var category = _context.Category.Find(id);
            return PartialView("_ConfirmDelete", category);
        }

        
        public ActionResult DeleteCategoryPost(int id)
        {
            try
            {
                var category = _context.Category.Find(id);
                _context.Remove(category);
                _context.SaveChanges();
                return Json(true);
            } catch
            {
                return Json(false);
            }
           
        }
    }
}
