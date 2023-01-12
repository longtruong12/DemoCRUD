using CRUDApp.Models.DTO;
using CRUDApp.Models;
using Microsoft.AspNetCore.Mvc;
using CRUDApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ConnectDB _context;
        public ProductController(ConnectDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllProduct()
        {
            var product = _context.Product.ToList();
            var jsonCategory = Json(product);
            return jsonCategory;
        }

        public ActionResult GetProductPartial(int? id)
        {
            var product = _context.Product.Find(id) ?? new Product();
            var data = _context.Category.ToList();
            var categories = data.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.ItemsCategory = categories;
            return PartialView("_CreateOrUpdateProduct", product);
        }


        public ActionResult CreateOrUpdateProduct(ProductCreateDTO modal)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Id = modal.Id,
                    Descreption = modal.Descreption,
                    Name = modal.Name,
                    CategoryId = modal.CategoryId
                };

                if (product.Id > 0)
                {
                    _context.Product.Update(product);

                }
                else
                {
                    _context.Product.Add(product);
                }
                _context.SaveChanges();
                return Json(true);
            }
            var data = _context.Category.ToList();
            var categories = data.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.ItemsCategory = categories;
            return PartialView("_CreateOrUpdateProduct", new Product());
        }

        public ActionResult DeleteProduct(int id)
        {
            //_context.Remove(id);
            var product = _context.Product.Find(id);
            return PartialView("_ConfirmDelete", product);
        }


        public ActionResult DeleteProductPost(int id)
        {
            try
            {
                var product = _context.Product.Find(id);
                _context.Remove(product);
                _context.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }
    }
}
