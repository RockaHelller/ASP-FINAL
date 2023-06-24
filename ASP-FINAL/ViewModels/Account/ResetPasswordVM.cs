using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ASP_FINAL.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }

    }
}
