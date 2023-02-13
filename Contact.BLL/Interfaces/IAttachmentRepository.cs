using Contact.DAL;
using Contact.VM;
using Microsoft.AspNetCore.Http;

namespace Contact.BLL.Interfaces
{
    public interface IAttachmentRepository : IRepositoryBase<CustomerAttachment>
    {
        public APIResult<AttachmentViewModel> GetAttachmentById(int attachmentId); //API
        public APIResult<AttachmentViewModel> GetAttachmentsByCustomer(int customerId); //API
        public AttachmentViewModel? AddAttachment(int customerId);
        public APIResult<AttachmentViewModel> AddAttachment(IFormFile attachmentFile, int customerId, int loggedUser); //API
        public bool AddAttachment(AttachmentViewModel attachmentVM, int loggedUser);
        public APIResult<AttachmentViewModel> DeleteAttachment(int id, int loggedUser);
        public APIResult<AttachmentViewModel> RestoreAttachment(int id, int loggedUser); //API
    }
}
