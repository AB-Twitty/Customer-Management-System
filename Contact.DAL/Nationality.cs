using System;
using System.Collections.Generic;

namespace Contact.DAL
{
    public partial class Nationality
    {
        public Nationality()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string NationalityName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
