using Project_PhoneStore.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PhoneStore.Models
{
    public class AdminModel : Account
    {
       public String info {  get; set; }
        

    }
}
