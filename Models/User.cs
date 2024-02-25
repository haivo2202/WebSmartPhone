using System.ComponentModel.DataAnnotations;

namespace Project_PhoneStore.Models
{
    public class User: Account
    {
        public List<Order> Orders { get; set; }
    }
}
