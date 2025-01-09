using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class User
    {
        public int Id { get; set; }
        //attribute sadhai mathi thapnea garinxa which is also data annotation
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]//yaha attribute use garera password lai password type ma dekha vaneko ho called as data annotation
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password lenght should be greater than 8")]

        public string Password { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }

    }
}