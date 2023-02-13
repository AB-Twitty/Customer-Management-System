using Contact.DAL;
using System.ComponentModel.DataAnnotations;

namespace Contact.VM
{
    public class ContactAddViewModel
    {
        public ContactAddViewModel() { }
        public ContactAddViewModel(CustomerContact contact)
        {
            id = contact.Id;
            customerId = contact.CustomerId;
            this.contact = contact.Contact;
            contactTypeId = contact.ContactTypeId;
        }

        private class CheckExistanceAttribute : ValidationAttribute
        {
            private string ProperityName { get; set; }

            public CheckExistanceAttribute(string properityName)
            {
                ProperityName = properityName;
            }

            protected override ValidationResult IsValid(object contact, ValidationContext validationContext)
            {
                Object instance = validationContext.ObjectInstance;
                Type type = instance.GetType();
                Object properityValue = type.GetProperty(ProperityName).GetValue(instance, null);
                ContactDBContext _context = new ContactDBContext();
                if (_context.CustomerContacts.Where(x => x.Contact == contact.ToString() && (x.Id.ToString() != properityValue.ToString() || x.Id == 0)).Any())
                    return new ValidationResult($"This Contact '{contact}' already exists.");
                else
                    return ValidationResult.Success;
            }
        }

        [Required]
        public int id { get; set; }

        [Required]
        public int customerId { get; set; }

        [Required]
        [CheckExistance("id")]
        public string contact { get; set; }

        [Required]
        public int contactTypeId { get; set; }
        [Required]
        public bool isActive { get; set; }

        public static implicit operator CustomerContact(ContactAddViewModel contactVM)
        {
            return new CustomerContact
            {
                Id = contactVM.id,
                CustomerId = contactVM.customerId,
                Contact = contactVM.contact,
                ContactTypeId = contactVM.contactTypeId,
                IsActive = contactVM.isActive,
            };
        }
    }
}
