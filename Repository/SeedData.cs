using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;

namespace Project_PhoneStore.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
                CategoryModel productnew = new CategoryModel { Name = "Máy mới 100%", Description = "Máy Mới full box", Status = true };

                CategoryModel productold = new CategoryModel { Name = "Máy cũ 99%", Description = "Máy cũ 99% tình trạng đẹp keng", Status = true };

                Model phonemodel = new Model { Name = "Smartphone", Description = "Our smartphone is cheapest", Status = true };

                Model tabletmodel = new Model { Name = "Tablet", Description = "Our Tablet is the best", Status = true };

                Model watchmodel = new Model { Name = "Smartwatch", Description = "Our Smart watch is the best", Status = true };


                BrandModel apple = new BrandModel { Name = "Apple", Description = "Apple is large Brand in the world", Status = true };

                BrandModel samsung = new BrandModel { Name = "Samsung", Description = "Samsung is large Brand in the world", Status = true };

                _context.Products.AddRange(

                new ProductModel { Name = "SAMSUNG GALAXY S9 NEW", Description = "GalaxyS9 FULL BOX NEW 100%", Image = "item1.jpg", Category = productnew, Brand = samsung, Model = phonemodel, Price = 19, Status = true },
                new ProductModel { Name = "IPHONE 15 PRO MAX NEW", Description = "Iphone 15 PRO MAX FULL BOX NEW 100%", Image = "item2.jpg", Category = productnew, Brand = apple, Model = phonemodel, Price = 25, Status = true },
                new ProductModel { Name = "SAMSUNG GALAXY S8 LIKE NEW", Description = "GalaxyS8 LIKE NEW 99%", Image = "item3.jpg", Category = productold, Brand = samsung, Model = phonemodel, Price = 13 , Status = true },
                new ProductModel { Name = "IPHONE 15 PLUS LIKE NEW", Description = "Iphone 15 PLUS LIKE NEW 99%", Image = "item4.jpg", Category = productold, Brand = apple, Model = phonemodel, Price = 20 , Status = true },
                new ProductModel { Name = "IPAD MINI 6 NEW", Description = "Ipad mini 6 128gb 100% full box new 100%", Image = "item6.jpg", Category = productnew, Brand = apple, Model = tabletmodel, Price = 26 , Status = true },
                new ProductModel { Name = "SAMSUNG GALAXY TAB A9 NEW", Description = "Samsung galaxy Tab A9 full box new 100%", Image = "item7.jpg", Category = productnew, Brand = samsung, Model = tabletmodel, Price = 20, Status = true },
                new ProductModel { Name = "IPAD MINI 6 LIKE NEW", Description = "Ipad mini 6 128gb lIKE NEW 99%", Image = "item6.jpg", Category = productold, Brand = apple, Model = tabletmodel, Price = 21, Status = true },
                new ProductModel { Name = "SAMSUNG GALAXY TAB A9 LIKE NEW", Description = "Samsung galaxy Tab A9 LIKE NEW 99%", Image = "item7.jpg", Category = productold, Brand = samsung, Model = tabletmodel, Price = 17, Status = true },
                 new ProductModel { Name = "APPLE WATCH SERIES 9 NEW", Description = "Apple watch series 9 FULL BOX NEW 100%", Image = "item8.jpg", Category = productnew, Brand = apple, Model = watchmodel, Price = 16, Status = true },
                  new ProductModel { Name = "SAMSUNG GALAXY WATCH 6 NEW", Description = "Samsung galaxy Watch 6 FULL BOX NEW 100%", Image = "item9.jpg", Category = productnew, Brand = samsung, Model = watchmodel, Price = 10, Status = true },
                    new ProductModel { Name = "APPLE WATCH SERIES 9 LIKE NEW", Description = "Apple watch series 9 FULL BOX NEW 100%", Image = "item8.jpg", Category = productold, Brand = apple, Model = watchmodel, Price = 8 , Status = true },
                     new ProductModel { Name = "SAMSUNG GALAXY WATCH 6 LIKE NEW", Description = "Samsung galaxy Watch 6 LIKE NEW 99%", Image = "item9.jpg", Category = productold, Brand = samsung, Model = watchmodel, Price = 6, Status = true }


                );
                _context.SaveChanges();
            }
        }
    }
}
