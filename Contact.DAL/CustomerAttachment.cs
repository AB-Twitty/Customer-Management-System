using System;
using System.Collections.Generic;

namespace Contact.DAL
{
    public partial class CustomerAttachment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AttachmentUrl { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifierId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual User Modifier { get; set; } = null!;
    }
}
