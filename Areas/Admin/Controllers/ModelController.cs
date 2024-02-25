using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Filters;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    public class ModelController : Controller
    {
        private readonly DataContext _dataContext;
        public ModelController(DataContext Context)
        {
            _dataContext = Context;


        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Models.OrderByDescending(p => p.Id).ToListAsync());
        }
        public async Task<IActionResult> Edit(int Id)
        {
            Model model = await _dataContext.Models.FindAsync(Id);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model model)
        {
            if (ModelState.IsValid)
            {
                //code du lieu
                model.Name = model.Name.Replace(" ", " ");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Name == model.Name);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Mau đã có trong database");
                    return View(model);
                }


                _dataContext.Add(model);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = " Thêm mau thành công ";
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

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model model)
        {
            if (ModelState.IsValid)
            {
                //code du lieu
                model.Name = model.Name.Replace(" ", "-");
                var slug = await _dataContext.Models.FirstOrDefaultAsync(p => p.Name == model.Name);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã có trong database");
                    return View(model);
                }


                _dataContext.Update(model);
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

            return View(model);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            Model model = await _dataContext.Models.FindAsync(Id);


            _dataContext.Models.Remove(model);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Danh mục đã xóa";
            return RedirectToAction("Index");

        }
    }
}
