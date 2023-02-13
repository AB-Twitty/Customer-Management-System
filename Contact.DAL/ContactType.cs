using System;
using System.Collections.Generic;

namespace Contact.DAL
{
    public partial class ContactType
    {
        public ContactType()
        {
            CustomerContacts = new HashSet<CustomerContact>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string ValidationExpression { get; set; } = null!;

        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
    }
}
