using System.ComponentModel.DataAnnotations;
using Contact.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.VM
{
    public class CustomerAddViewModel
    {
        private class HasSixteenYearsOrMoreAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                int age = 0;
                age = (int)((DateTime.Today - (DateTime)value).TotalDays / 365);
                if (age >= 16)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Must be at least 16 years!");
            }
        }

        private class CheckExistanceAttribute : ValidationAttribute
        {
            private string ProperityName { get; set; }

            public CheckExistanceAttribute(string properityName)
            {
                ProperityName = properityName;
            }

            protected override ValidationResult IsValid(object nationalId, ValidationContext validationContext)
            {
                Object instance = validationContext.ObjectInstance;
                Type type = instance.GetType();
                Object properityValue = type.GetProperty(ProperityName).GetValue(instance, null);
                ContactDBContext _context = new ContactDBContext();
                if (_context.Customers.Where(x => x.NationalId == nationalId.ToString() && (x.Id.ToString() != properityValue.ToString() || x.Id == 0)).Any())
                    return new ValidationResult($"This National ID '{nationalId}' already exists.");
                else
                    return ValidationResult.Success;
            }
        }

        [Required]
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string firstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string lastName { get; set; } = null!;

        [Required]
        [Display(Name = "National ID")]
        [StringLength(14, ErrorMessage = "National ID must be 14 numbers ", MinimumLength = 14)]
        [CheckExistance("id")]
        public string nationalId { get; set; } = null!;

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [HasSixteenYearsOrMore(ErrorMessage = "Must be at least 16 years!")]
        public DateTime birthDate { get; set; }

        [Required]
        public int nationalityId { get; set; }

        public bool isActive { get; set; }

        public IFormFile? imageFile { get; set; }

        public static implicit operator Customer(CustomerAddViewModel customerVM)
        {
            return new Customer
            {
                FirstName = customerVM.firstName,
                LastName = customerVM.lastName,
                BirthDate = customerVM.birthDate,
                NationalId = customerVM.nationalId,
                NationalityId = customerVM.nationalityId,
                IsActive = customerVM.isActive,
            };
        }

        public void copyTo(Customer customer)
        {
            customer.FirstName = this.firstName;
            customer.LastName = this.lastName;
            customer.BirthDate = this.birthDate;
            customer.NationalId = this.nationalId;
            customer.NationalityId = this.nationalityId;
            customer.IsActive = this.isActive;
        }

    }
}
