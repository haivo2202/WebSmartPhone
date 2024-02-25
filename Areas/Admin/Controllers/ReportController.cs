using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment; //để load files trong asp
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        public ReportController(SignInManager<Account> signInManager, UserManager<Account> userManager, DataContext _context, IWebHostEnvironment webHostEnvironment)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._dataContext = _context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Order> orders = this._dataContext.orders.ToList();
            ReportModel model = new ReportModel();
            model.GrandTotal = (int)orders.Sum(x => x.GrandTotal);
            model.TotalOrder = this._dataContext.orders.Count();
            model.TotalUser = this._dataContext.users.Count();
            model.TotalOrderProcess = this._dataContext.orders.Where(o => o.Status == "DANG XU LY").Count();
            return View(model);
        }

    }
}
