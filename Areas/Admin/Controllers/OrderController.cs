using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Filters;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;
using Project_PhoneStore.Repository.Components;
using System;

namespace Project_PhoneStore.Areas.Admin.Controllers
{

    [AuthorizeAdmin]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment; //để load files trong asp
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        public OrderController(SignInManager<Account> signInManager, UserManager<Account> userManager, DataContext _context, IWebHostEnvironment webHostEnvironment)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._dataContext = _context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var orders = this._dataContext.orders.ToList();
            return View(orders);
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Order order = await _dataContext.orders.FindAsync(Id);
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                //code du lieu
                order.Status = order.Status.Replace(" ", "-");
               


                _dataContext.Update(order);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = " Cập nhật danh mục thành công ";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Có một số thứ bị lỗi";
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

            return View(order);
        }
    
        public async Task<IActionResult> Delete(int Id)
        {
            Order order = await _dataContext.orders.FindAsync(Id);

            _dataContext.orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Order da bi xoa";
            return RedirectToAction("Index", "Order");

        }
    

    }
}

