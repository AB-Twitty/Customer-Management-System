using System.ComponentModel.DataAnnotations;
using Contact.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.VM
{
    public class CustomerViewModel
    {
        private class HasSixteenYearsOrMoreAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                int age = 0;
                age = (int)((DateTime.Today - (DateTime)value).TotalDays/365);
                if (age>=16)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Must be at least 16 years!");
            }
        }
        public CustomerViewModel()
        {
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "National ID")]
        [StringLength(14, ErrorMessage = "National ID must be 14 numbers ", MinimumLength = 14)]
        [Remote("VerifyNationalId", "Customer")]
        public string NationalId { get; set; } = null!;

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [HasSixteenYearsOrMore(ErrorMessage = "Must be at least 16 years!")]
        public DateTime BirthDate { get; set; }

        [Required]
        public int NationalityId { get; set; }

        public bool IsActive { get; set; }
        public IFormFile? ImageFile { get; set; }
        public List<NationalityViewModel>? Nationalities { get; set; }
        public bool IsDeleted { get; set; }

        public int? CreatorId { get; set; } = null;

        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; } = null;

        public int? ModifierId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedDate { get; set; } = null;
        public string? CustomerImageUrl { get; set; } = null;

        public virtual User? Creator { get; set; } = null!;

        public virtual User? Modifier { get; set; } = null!;

        public virtual NationalityViewModel? Nationality { get; set; } = null;

        public virtual ICollection<ContactViewModel>? CustomerContacts { get; set; } = null;
        public virtual ICollection<AttachmentViewModel>? CustomerAttachments { get; set; } = null;


        public CustomerViewModel(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            NationalId = customer.NationalId;
            BirthDate = customer.BirthDate;
            NationalityId = customer.NationalityId;
            IsActive = customer.IsActive;
            IsDeleted = (bool)customer.IsDeleted;
            CreatorId = customer.CreatorId;
            CreationDate = customer.CreationDate;
            ModifierId = customer.ModifierId;
            LastModifiedDate = customer.LastModifiedDate;
            CustomerImageUrl = customer.CustomerImageUrl;
            Creator = customer.Creator;
            Modifier = customer.Modifier;
            Nationality = new NationalityViewModel(customer.Nationality);

            CustomerContacts = new List<ContactViewModel>();
            foreach (var contact in customer.CustomerContacts)
                CustomerContacts.Add(new ContactViewModel(contact));

            CustomerAttachments = new List<AttachmentViewModel>();
            foreach (var attachment in customer.CustomerAttachments)
                CustomerAttachments.Add(new AttachmentViewModel(attachment));
        }

        public static implicit operator Customer(CustomerViewModel customerVM)
        {
            return new Customer
            {
                Id = customerVM.Id,
                FirstName = customerVM.FirstName,
                LastName = customerVM.LastName,
                NationalId = customerVM.NationalId,
                BirthDate = customerVM.BirthDate,
                NationalityId = customerVM.NationalityId,
                IsActive = customerVM.IsActive,
                IsDeleted = customerVM.IsDeleted,
            };
        }
    }
}
