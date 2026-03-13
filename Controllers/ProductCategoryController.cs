using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Data;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Models;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly SamOnlineShoppingStoreDBContext _dbContext;

        public ProductCategoryController(SamOnlineShoppingStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            // If SearchPhrase is null, empty or whitespace, set it to an empty string
            string search = string.IsNullOrWhiteSpace(SearchPhrase) ? "" : SearchPhrase;

            // Store the search phrase in session state as the last product search
            HttpContext.Session.SetString("LastProductsSearch", search);

            string query = "SELECT * FROM Products" +
                            $"WHERE ItemName LIKE @Search" +
                            $"OR Unit LIKE @Search" +
                            $"OR Price LIKE @Search";
                            /*$"OR Category LIKE @SEarch";*/

            var parameters = new SqlParameter("Search", $"%{search}%");

            // Retrieve products from the database that have a ItemName containing the
            // search phrase, including their productsCategory
            var products = _dbContext.Products.Where(p => p.ItemName.Contains(SearchPhrase))
                                                    /*.Include(p => p.Category)*/
                                                    .FirstOrDefault();
            return View("ShowSearchResults", products);
        }

        public ActionResult Baked(int Id)
        {
            var baked = _dbContext.Products.Where(b => b.CategoryId == Id);
            return View(baked);
        }

        public ActionResult Beverage(int Id)
        {
            var beverage = _dbContext.Products.Where(b => b.CategoryId == Id);
            return View(beverage);
        }

        public ActionResult ColdMeat(int Id)
        {
            var chilled = _dbContext.Products.Where(c => c.CategoryId == Id);
            return View(chilled);
        }

        public ActionResult Dairy(int Id)
        {
            var dairies = _dbContext.Products.Where(d => d.CategoryId == Id);
            return View(dairies);
        }

        public ActionResult FreshFoods(int Id)
        {
            var produce = _dbContext.Products.Where(p => p.CategoryId == Id);
            return View(produce);
        }

        public ActionResult Frozen(int Id)
        {
            var freeze = _dbContext.Products.Where(f => f.CategoryId == Id);
            return View(freeze);
        }

        public ActionResult Pantry(int Id)
        {
            var pantries = _dbContext.Products.Where(p => p.CategoryId == Id);
            return View(pantries);
        }

        public ActionResult SelfCare(int Id)
        {
            var care = _dbContext.Products.Where(c => c.CategoryId == Id);
            return View(care);
        }
    }
}
