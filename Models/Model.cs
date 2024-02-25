
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_PhoneStore.Models
{
    public class Model
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập vào tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập vào Mô tả sản phẩm")]
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
