using Project_PhoneStore.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PhoneStore.Models
{
	public class ProductModel
	{

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập vào tên sản phẩm")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập vào giá sản phẩm")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "int")]
        public int Price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một thương hiệu")]
        public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục")]
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        public BrandModel Brand { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một Mau")]
        public int ModelId { get; set; }
        
        public bool Status { get; set; }

        public Model Model { get; set; }
        public String Image { get; set; } = "noimage.jpg";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
