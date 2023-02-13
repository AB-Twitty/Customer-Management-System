using System;
using System.Collections.Generic;

namespace Contact.DAL
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAttachments = new HashSet<CustomerAttachment>();
            CustomerContacts = new HashSet<CustomerContact>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int NationalityId { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifierId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string? CustomerImageUrl { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual User Modifier { get; set; } = null!;
        public virtual Nationality Nationality { get; set; } = null!;
        public virtual ICollection<CustomerAttachment> CustomerAttachments { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
    }
}
