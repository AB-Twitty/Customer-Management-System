using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Contact.VM;

namespace Contact.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilterAttribute))]
    public class AttachmentController : ApiBaseController
    {
        public AttachmentController(IRepositoryWrapper repo) : base(repo) { }

        [HttpGet("GetAttachment/{attachmentId}")]
        public APIResult<AttachmentViewModel> GetAttachmentById(int attachmentId)
        {
            return _repo.attachment.GetAttachmentById(attachmentId);
        }

        [HttpGet("GetAttachmentsFor/{customerId}")]
        public APIResult<AttachmentViewModel> GetAttachmentsByCustomer(int customerId)
        {
            return _repo.attachment.GetAttachmentsByCustomer(customerId);
        }

        [HttpGet("DeleteAttachment/{attachmentId}")]
        public APIResult<AttachmentViewModel> DeleteAttachment(int attachmentId)
        {
            return _repo.attachment.DeleteAttachment(attachmentId, _loggedUser);
        }

        [HttpGet("RestoreAttachment/{attachmentId}")]
        public APIResult<AttachmentViewModel> RestoreAttachment(int attachmentId)
        {
            return _repo.attachment.RestoreAttachment(attachmentId, _loggedUser);
        }

        [HttpPost("AddAttachmentTo/{customerId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<AttachmentViewModel> AddAttachment(IFormFile attachmentFile, int customerId)
        {
            return _repo.attachment.AddAttachment(attachmentFile, customerId, _loggedUser);
        }
    }
}
