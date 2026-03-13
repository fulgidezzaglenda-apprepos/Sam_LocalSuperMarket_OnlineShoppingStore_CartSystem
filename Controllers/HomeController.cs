using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Data;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Models;
using System.Diagnostics;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SamOnlineShoppingStoreDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, SamOnlineShoppingStoreDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult AboutUS()
        {
            return View();
        }

        public ActionResult CustomerSupport()
        {
            return View();
        }

        public ActionResult CartHistory() 
        {
            // Get the user Id out of the session and store it.
            var userId = HttpContext.Session.GetInt32("ID");

            // Check if the user is not logged in or has an invalid ID stored.
            if (userId == null || userId < 0 || !HttpContext.User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                // Get both finalized and cancelled shopping carts for the current user from the database.
                var finalizedCarts = _dbContext.ShoppingCarts.Include(c => c.CartUser)
                    .Where(c => c.UserId == userId && c.IsFinalised).ToList();

                var cancelledCarts = _dbContext.ShoppingCarts.Include(c => c.CartUser)
                    .Where(c => c.UserId == userId && c.IsCancelled).ToList();

                return View(new Tuple<List<ShoppingCart>, List<ShoppingCart>>(finalizedCarts, cancelledCarts));
            }
        }

        public ActionResult Category()
        {
            var category = _dbContext.Categories.AsEnumerable();
            return View(category);
        }

        [HttpPost]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            // If SearchPhrase is null, empty or whitespace, set it to an empty string
            string search = string.IsNullOrWhiteSpace(SearchPhrase) ? "" : SearchPhrase;

            // Store the search phrase in session state as the last book search
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}