using System.ComponentModel.DataAnnotations;

namespace Project_PhoneStore.Models
{
	public class OrderViewModel
	{
		public string ReceiverPhoneNumber { get; set; }

		public string Status { get; set; }

		public string ShippingAddress { get; set; }


		public string Note { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public List<CartItemModel> CartItemModels { get; set; }
		public int GrandTotal { get; set; }
	}
}
