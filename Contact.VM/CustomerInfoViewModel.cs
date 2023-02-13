using System.ComponentModel.DataAnnotations;
using Contact.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.VM
{
    public class CustomerInfoViewModel
    {
        
        public CustomerInfoViewModel() { }
        public CustomerInfoViewModel(Customer customer)
        {
            Id= customer.Id;
            FirstName= customer.FirstName;
            LastName= customer.LastName;   
            NationalId= customer.NationalId;
            BirthDate= customer.BirthDate;
            NationalityId= customer.NationalityId;
            IsActive= customer.IsActive;
            IsDeleted= customer.IsDeleted;
            CustomerImageURL = customer.CustomerImageUrl;
        }
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name:")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name:")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "National ID:")]
        [StringLength(14, ErrorMessage = "National ID must be 14 numbers ", MinimumLength = 14)]
        [Remote("VerifyNationalId", "Customer")]
        public string NationalId { get; set; } = null!;

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        public int NationalityId { get; set; }

        public bool IsActive { get; set; }
        
        public bool? IsDeleted { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? CustomerImageURL { get; set; }

        public static implicit operator Customer(CustomerInfoViewModel customerVM)
        {
            return new Customer
            {
                Id = customerVM.Id,
                FirstName = customerVM.FirstName,
                LastName = customerVM.LastName,
                BirthDate = customerVM.BirthDate,
                NationalId = customerVM.NationalId,
                NationalityId = customerVM.NationalityId,
                IsActive = customerVM.IsActive,
                IsDeleted = customerVM.IsDeleted,
                CustomerImageUrl = customerVM.CustomerImageURL
            };
        }
    }
    } 
