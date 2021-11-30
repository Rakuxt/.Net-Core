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
            if (category.DisplayOrder.ToString() == category.Name)
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

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFind = dbContext.Categories.Find(id);
            //var categorySingle = dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.DisplayOrder.ToString() == category.Name)
            {
                ModelState.AddModelError("CustomErrors", "The DisplayOrder cannot exactly match the Name.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFind = dbContext.Categories.Find(id);
            //var categorySingle = dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
