using System.ComponentModel.DataAnnotations;
using Contact.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Contact.VM
{
    public class UserInfoViewModel
    {
        public UserInfoViewModel() { }
        public UserInfoViewModel(User user)
        {
            userId = user.Id;
            name = user.Name;
            username = user.Username;
            email = user.Email;
        }
        private class CheckExistanceAttribute : ValidationAttribute
        {
            private string ProperityName { get; set;}

            public CheckExistanceAttribute(string properityName)
            {
                ProperityName = properityName;
            }

            protected override ValidationResult IsValid(object emailOrUsername, ValidationContext validationContext)
            {
                Object instance = validationContext.ObjectInstance;
                Type type = instance.GetType();
                Object properityValue = type.GetProperty(ProperityName).GetValue(instance, null);
                ContactDBContext _context = new ContactDBContext();
                string EmailOrUsername = emailOrUsername.ToString();
                if (_context.Users.Where(x => x.Username == EmailOrUsername && (x.Id.ToString()!=properityValue.ToString() || x.Id==0)).Any())
                    return new ValidationResult($"A user with username '{EmailOrUsername}' already exists.");
                else if (_context.Users.Where(x => x.Email == EmailOrUsername && (x.Id.ToString()!=properityValue.ToString() || x.Id==0)).Any())
                    return new ValidationResult($"A user with E-mail '{EmailOrUsername}' already exists.");
                else
                    return ValidationResult.Success;
            }
        }

        [Required]
        public int userId { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "UserName")]
        [CheckExistance("userId")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [CheckExistance("userId")]
        public string email { get; set; }
    }
}
