using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Filters;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Controllers;
public class AdminController : Controller
{
    private readonly SignInManager<Account> _signInManager;
    private readonly UserManager<Account> _userManager;
    private readonly IUserStore<Account> _userStore;
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment; //để load files trong asp

    public AdminController(SignInManager<Account> signInManager, UserManager<Account> userManager, IUserStore<Account> userStore, DataContext _contex, IWebHostEnvironment webHostEnvironment)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userStore = userStore;
        _context = _contex;
        _webHostEnvironment = webHostEnvironment;
    }
    [AuthorizeAdmin]
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Login()
    {

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginAdminViewModel model)
    {
        string returnUrl = Url.Content("~/admin");

        // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (!isAdmin)
                {
                    return RedirectToAction("Logout");
                }
                return LocalRedirect(returnUrl); // Replace with the desired redirect URL
            }



            // if (result.RequiresTwoFactor)
            // {
            //     return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            // }
            // if (result.IsLockedOut)
            // {
            // _logger.LogWarning("User account locked out.");
            //    return RedirectToPage("./Lockout");
            // }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }
        return View();
    }



    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterAdminViewModel model)
    {
        string returnUrl = Url.Content("~/Admin/Login");
        //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (ModelState.IsValid)
        {
            var admin = new AdminModel
            {
                UserName = model.Email,
                Email = model.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            //await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(admin, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
                TempData["Message"] = "Tạo thành công";
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View();
    }

    public async Task<IActionResult> Profile()
    {
        var admin = await _userManager.GetUserAsync(User);
        return View(admin);
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    public IActionResult UpdateProfile()
    {
        return View();
    }
    public async Task<IActionResult> UpdateProfile1(AdminViewModel model)
    {
        var admin = await _userManager.GetUserAsync(User);
        if (ModelState.IsValid)
        {
           
            if (model.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "assets/image");
                string imageName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);

                FileStream fs = new FileStream(filePath, FileMode.Create);
                await model.ImageUpload.CopyToAsync(fs);
                fs.Close();
                model.Image = imageName;
            }

            admin.UserName = model.Name;
            admin.PhoneNumber = model.PhoneNumber;
            admin.Email = model.Email;
            admin.Image = model.Image;
            await _userManager.UpdateAsync(admin);
            TempData["success"] = " Cập nhật profile thành công ";
            return RedirectToAction("Profile","Admin");



        }
        else
        {
            TempData["error"] = "Co mot vai thu dang bi loi";
            List<string> errors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            string errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        return View(admin);
    }


}





