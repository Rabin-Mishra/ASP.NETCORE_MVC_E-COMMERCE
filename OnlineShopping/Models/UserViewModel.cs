using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class UserViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password lenght should be greater than 8")]

        public string Password {  get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password dont match")]
        public string ConfirmPassword { get; set; }
    }
}
