using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext Context)
		{
			_dataContext = Context;
		}
        public IActionResult Index(int id)
        {
            var products = _dataContext.Products.Where(P => P.BrandId == id).ToList();
            return View(products);

        }
    }
}
