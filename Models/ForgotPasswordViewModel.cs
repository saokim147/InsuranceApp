using System.ComponentModel.DataAnnotations;

namespace InsuranceWebApp.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Xin hãy nhập email")]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;
    }
}
