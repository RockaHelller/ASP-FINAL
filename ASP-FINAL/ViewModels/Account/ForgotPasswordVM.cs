using System.ComponentModel.DataAnnotations;

namespace ASP_FINAL.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}
