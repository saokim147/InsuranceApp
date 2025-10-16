using System.ComponentModel.DataAnnotations;

namespace InsuranceWebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Yêu cầu Email.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yêu cầu mật khẩu.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lưu lại?")]
        public bool RememberMe { get; set; }
    }
}
