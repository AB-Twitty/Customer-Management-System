using System.ComponentModel.DataAnnotations;
using Contact.DAL;
using Microsoft.AspNetCore.Mvc;


namespace Contact.VM
{
    public class UserViewModel : UserInfoViewModel
    {
        public UserViewModel()
        {
        }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }

        public UserViewModel(User user)
        {
            userId = user.Id;
            name = user.Name;
            username = user.Username;
            email = user.Email;
            password = user.Password;
        }

        public static implicit operator User(UserViewModel userVM)
        {
            return new User
            {
                Id = userVM.userId,
                Name = userVM.name,
                Username = userVM.username,
                Email = userVM.email,
                Password = userVM.password,
            };
        }
    }
}
