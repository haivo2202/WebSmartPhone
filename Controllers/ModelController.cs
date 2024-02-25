using Microsoft.AspNetCore.Mvc;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Controllers
{
    public class ModelController : Controller
    {
            private readonly DataContext _dataContext;
            public ModelController(DataContext Context)
            {
                _dataContext = Context;
            }
            public IActionResult Index(int id)
            {
                var products = _dataContext.Products.Where(P => P.ModelId == id).ToList();
                return View(products);
            }
        
    }
}
