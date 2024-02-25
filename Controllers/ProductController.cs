using Microsoft.AspNetCore.Mvc;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Controllers
{
	public class ProductController : Controller
	{

		private readonly DataContext _dataContext;

		public ProductController(DataContext Context)
		{
			_dataContext = Context;
		}
		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Details(int Id)
        {
            ProductModel product = this._dataContext.Products.Where(p => p.Id == Id).FirstOrDefault();
            return View(product);
        }
    }
}
