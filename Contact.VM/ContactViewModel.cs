using System.ComponentModel.DataAnnotations;
using Contact.DAL;

namespace Contact.VM
{
    public class ContactViewModel
    {
        public ContactViewModel() { }
        //public List<ContactType> ContactTypes { get; set; }
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public int ContactTypeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }

        public virtual ContactTypeViewModel ContactType { get; set; }
        public virtual UserInfoViewModel Creator { get; set; }
        //public virtual CustomerInfoViewModel Customer { get; set; }
        public virtual UserInfoViewModel Modifier { get; set; }

        public ContactViewModel(CustomerContact contact)
        {
            Id = contact.Id;
            CustomerId = contact.CustomerId;
            Contact = contact.Contact;
            IsActive = contact.IsActive;
            IsDeleted = (bool)contact.IsDeleted;
            CreationDate = contact.CreationDate;
            LastModifiedDate = contact.LastModifiedDate;
            ContactTypeId = contact.ContactTypeId;
            ContactType = new ContactTypeViewModel(contact.ContactType);
            Creator = new UserInfoViewModel(contact.Creator);
            Modifier = new UserInfoViewModel(contact.Modifier);
            //Customer = new CustomerInfoViewModel(contact.Customer);
        }

        public static implicit operator CustomerContact(ContactViewModel contactVM)
        {
            return new CustomerContact
            {
                CustomerId = contactVM.CustomerId,
                Contact = contactVM.Contact,
                ContactTypeId = contactVM.ContactTypeId,
                IsActive = contactVM.IsActive,
                IsDeleted = contactVM.IsDeleted,
            };
        }
    }
}
