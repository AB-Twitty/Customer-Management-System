using System;
using System.Collections.Generic;

namespace Contact.DAL
{
    public partial class User
    {
        public User()
        {
            CustomerAttachmentCreators = new HashSet<CustomerAttachment>();
            CustomerAttachmentModifiers = new HashSet<CustomerAttachment>();
            CustomerContactCreators = new HashSet<CustomerContact>();
            CustomerContactModifiers = new HashSet<CustomerContact>();
            CustomerCreators = new HashSet<Customer>();
            CustomerModifiers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<CustomerAttachment> CustomerAttachmentCreators { get; set; }
        public virtual ICollection<CustomerAttachment> CustomerAttachmentModifiers { get; set; }
        public virtual ICollection<CustomerContact> CustomerContactCreators { get; set; }
        public virtual ICollection<CustomerContact> CustomerContactModifiers { get; set; }
        public virtual ICollection<Customer> CustomerCreators { get; set; }
        public virtual ICollection<Customer> CustomerModifiers { get; set; }
    }
}
