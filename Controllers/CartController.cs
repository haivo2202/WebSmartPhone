using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Project_PhoneStore.Models;
using Project_PhoneStore.Models.ViewModels;
using Project_PhoneStore.Repository;
using Project_PhoneStore.Repository.Components;


namespace Project_PhoneStore.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly SignInManager<Account> _signInManager;
		private readonly UserManager<Account> _userManager;


		public CartController(DataContext _Context, SignInManager<Account> signInManager, UserManager<Account> userManager)
		{
			_dataContext = _Context;
			this._signInManager = signInManager;
			this._userManager = userManager;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVm = new()
			{
				CartItems = cartItems,
				GrandTotal = (int)cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVm);
		}

		//Hàm thêm sản phẩm
		public async Task<IActionResult> Add(int Id)
		{
		
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			//Thêm sản phẩm và giỏ hàng
			if (cartItems == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else //nếu đã có rồi sản phẩm sẽ tăng lên 1
			{
				cartItems.Quantity += 1;
			}
			//Lữu trữ dữ liệu liêu giỏ hàng(cart) vào Sesion Cart
			HttpContext.Session.SetJson("Cart", cart);

			TempData["success"] = "Thêm sản phẩm vào giỏ hàng thành công";
			//sau khi thêm thành công sẽ trả về trang trước đó
			return Redirect(Request.Headers["Referer"].ToString());
		}
		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault(); //Lấy ra sản phẩm bằng "Id"

			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Giảm sản phẩm thành công";
			return RedirectToAction("Index");
		}
		//Thêm sản phẩm
		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault(); //Lấy ra sản phẩm bằng "Id"

			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Thêm sản phẩm thành công";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			cart.RemoveAll(p => p.ProductId == Id);

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);

			}
			TempData["success"] = "Xóa sản phẩm thành công";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Clear(int Id)
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Xóa tất cả sản phẩm thành công";
			return RedirectToAction("Index");
		}

	}
}

