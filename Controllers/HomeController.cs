using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;
using System.Diagnostics;

namespace Project_PhoneStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
	   public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
			_dataContext = context;
		}

        public IActionResult Index(string searchTerm)
        {
            var products = _dataContext.Products.ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {

                var searchResults = _dataContext.Products
                    .Where(p => p.Name.ToUpper().Contains(searchTerm.ToUpper()))
                    .ToList();

                ViewData["SearchResults"] = searchResults;

                return View("SearchResults", searchResults);
            }
            //List<ProductModel> products = _dataContext.Products.Include("Category").Include("Brand").ToList();

			return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if (statuscode == 404)
            {
                return View("NotFound"); 
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
           
        }
    }
}
