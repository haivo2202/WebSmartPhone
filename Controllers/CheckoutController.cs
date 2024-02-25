using Microsoft.AspNetCore.Mvc;
using Project_PhoneStore.Models.ViewModels;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository.Components;
using Microsoft.AspNetCore.Identity;
using Project_PhoneStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Project_PhoneStore.Controllers
{

	public class CheckoutController : Controller
	{
		private readonly SignInManager<Account> _signInManager;
		private readonly UserManager<Account> _userManager;
		private readonly DataContext _context;
		public CheckoutController(SignInManager<Account> signInManager, UserManager<Account> userManager, DataContext _contex)
		{
			this._signInManager = signInManager;
			this._userManager = userManager;
			this._context = _contex;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			OrderViewModel cartVm = new()
			{
				CartItemModels = cartItems,
				GrandTotal = (int)cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVm);
		}

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

		public IActionResult Checkout(OrderViewModel model)
		{

			if (!_signInManager.IsSignedIn(User))
			{
				return RedirectToAction("Login", "User");
			}
			
			string username = _userManager.GetUserName(User);
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			if(cartItems.Count == 0)
			{
				return RedirectToAction("Index", "Cart");
			}
			OrderViewModel cartVm = new()
			{
				CartItemModels = cartItems,
				GrandTotal = (int)cartItems.Sum(x => x.Quantity * x.Price)
			};
            Order order1 = new Order();
			order1.Name = username;
			order1.ReceiverPhoneNumber = model.ReceiverPhoneNumber;
			order1.ShippingAddress = model.ShippingAddress;
			order1.Note = model.Note;
			order1.GrandTotal = cartVm.GrandTotal;
			order1.Status = "Đang Xử Lý";
			order1.CreatedAt = DateTime.Now;
			_context.orders.Add(order1);
			_context.SaveChanges();

			return RedirectToAction("Confirm");
        }

		public IActionResult Confirm()
		{
			if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "User");
            }
			string userid = _userManager.GetUserName(User);
			var orders = this._context.orders.Where(x => x.Name == userid).ToList();
			return View(orders);

        }

		public IActionResult OrderHistory()
		{
			if (!_signInManager.IsSignedIn(User))
			{
				return RedirectToAction("Login", "User");
			}
			string userid = _userManager.GetUserName(User);
			var orders = this._context.orders.Where(x => x.Name == userid).ToList();
			return View(orders);

		}




	}
}


