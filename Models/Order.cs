using System.ComponentModel.DataAnnotations;

namespace Project_PhoneStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string ReceiverPhoneNumber { get; set; }

        public string Name { get; set; }


        public string Status { get; set; }

        public string ShippingAddress { get; set; }


		public string Note { get; set; }
		public DateTime CreatedAt { get; set; }

        public List<CartItemModel> CartItemModels { get; set; }
		public int GrandTotal { get; set; }
	}
}
