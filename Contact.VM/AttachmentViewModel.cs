using Contact.DAL;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contact.VM
{
    public class AttachmentViewModel
    {
        public AttachmentViewModel() { }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AttachmentUrl { get; set; } = null!;
        public IFormFile AttachmentFile { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual UserInfoViewModel Creator { get; set; } = null!;
        public virtual CustomerInfoViewModel Customer { get; set; } = null!;
        public virtual UserInfoViewModel Modifier { get; set; } = null!;

        public AttachmentViewModel(CustomerAttachment attachment) 
        {
            Id = attachment.Id; 
            CustomerId = attachment.CustomerId;
            AttachmentUrl = attachment.AttachmentUrl;
            IsDeleted = (bool)attachment.IsDeleted;
            CreationDate = attachment.CreationDate;
            LastModifiedDate = attachment.LastModifiedDate;
            Creator = new UserInfoViewModel(attachment.Creator);
            Modifier = new UserInfoViewModel(attachment.Modifier);
            Customer = new CustomerInfoViewModel(attachment.Customer);
        }

        public static implicit operator CustomerAttachment(AttachmentViewModel attachmentVM)
        {
            return new CustomerAttachment
            {
                CustomerId = attachmentVM.CustomerId,
                AttachmentUrl = attachmentVM.AttachmentUrl,
                IsDeleted = attachmentVM.IsDeleted,
                CreationDate = attachmentVM.CreationDate,
                LastModifiedDate = attachmentVM.LastModifiedDate,
            };
        }
    }
}
