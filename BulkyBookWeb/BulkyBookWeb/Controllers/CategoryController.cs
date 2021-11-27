using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryController(ApplicationDbContext db)
        {
            this.dbContext = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = dbContext.Categories.ToList();
            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.DisplayOrder.ToString() == category.Name)
            {
                ModelState.AddModelError("CustomErrors", "The DisplayOrder cannot exactly match the Name.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
