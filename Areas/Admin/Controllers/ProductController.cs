using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Filters;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_PhoneStore.Areas.Admin.Controllers
{
    
    [Area("Admin")]
	public class ProductController : Controller
	{

		private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment; //để load files trong asp
        public ProductController(DataContext Context, IWebHostEnvironment webHostEnvironment)
        {
			_dataContext = Context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).Include(p => p.Model).ToListAsync());
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
            ViewBag.Models = new SelectList(_dataContext.Models, "Id", "Name");



            return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel product)
		{
			ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
            ViewBag.Models = new SelectList(_dataContext.Models, "Id", "Name", product.ModelId);

            if (ModelState.IsValid)
			{
                //code du lieu
                product.Name = product.Name.Replace(" ", "-");
                var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
                if (slug != null)
                {
                    ModelState.AddModelError("","sản phẩm đã có trong database");
                    return View(product);
                }
				
					if(product.ImageUpload !=null)
					{
						string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath,"assets/image");
						string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
						string filePath = Path.Combine(uploadsDir, imageName);

						FileStream fs = new FileStream(filePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fs);
						fs.Close();
						product.Image = imageName;
					}
					_dataContext.Add(product);
					 await _dataContext.SaveChangesAsync();
					TempData["success"] = " Thêm sản phẩm thành công ";
					 return RedirectToAction("Index");

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
           
            return View(product);
        }
      public async Task<IActionResult> Edit(int Id)
		{

            ProductModel product = await _dataContext.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
            ViewBag.Models = new SelectList(_dataContext.Models, "Id", "Name", product.ModelId);

            return View(product);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,ProductModel product)
        {
            List<CategoryModel> categories = this._dataContext.Categories.ToList();
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
            ViewBag.Models = new SelectList(_dataContext.Models, "Id", "Name", product.ModelId);


            if (ModelState.IsValid)
            {
                //code du lieu
                product.Name = product.Name.Replace(" ", " ");
                var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
                if (slug != null)
                {
                    ModelState.AddModelError("", "sản phẩm đã có trong database");
                    return View(product);
                }

                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "assets/image");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;
                }
                _dataContext.Update(product);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = " Cập nhật sản phẩm thành công ";
                return RedirectToAction("Index");

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

            return View(product);
        }
     
        public async Task<IActionResult> Delete(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);

            if (!string.Equals(product.Image, "noimage.jpg"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "assets/image");
                string oldfileImage = Path.Combine(uploadsDir, product.Image);
                if (System.IO.File.Exists(oldfileImage))
                {
                    System.IO.File.Delete(oldfileImage);
                }
            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Sản phẩm đã xóa";
            return RedirectToAction("Index");

        }
    }
}
