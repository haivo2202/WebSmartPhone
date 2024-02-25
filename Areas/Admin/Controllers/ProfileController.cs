using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment; //để load files trong asp
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        public ProfileController(SignInManager<Account> signInManager, UserManager<Account> userManager, DataContext _context, IWebHostEnvironment webHostEnvironment)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._dataContext = _context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
