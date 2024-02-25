using Project_PhoneStore.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PhoneStore.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; } = "noimage.jpg";
        public string Password { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
