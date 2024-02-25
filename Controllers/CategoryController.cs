using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Controllers
{
	public class CategoryController : Controller
	{
		private readonly DataContext _dataContext;
		public CategoryController(DataContext Context)
		{
			_dataContext = Context;
		}
        public IActionResult Index(int id)
        {
            var products = _dataContext.Products.Where(P => P.CategoryId == id && P.Model.Name == "Smartphone").ToList();
            return View(products);
        }
    }
}
