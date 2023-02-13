using System.ComponentModel.DataAnnotations;

namespace Contact.VM
{
    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
    }
}
