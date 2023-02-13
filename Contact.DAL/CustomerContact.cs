using System;
using System.Collections.Generic;

namespace Contact.DAL
{
    public partial class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ContactTypeId { get; set; }
        public string Contact { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifierId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual ContactType ContactType { get; set; } = null!;
        public virtual User Creator { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual User Modifier { get; set; } = null!;
    }
}
