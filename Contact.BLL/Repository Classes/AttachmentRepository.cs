using Contact.BLL.Interfaces;
using Contact.DAL;
using Contact.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Contact.BLL.Repository_Classes
{
    internal class AttachmentRepository : RepositoryBase<CustomerAttachment>, IAttachmentRepository
    {
        private readonly IRepositoryWrapper _repo;
        public AttachmentRepository(ContactDBContext context, IRepositoryWrapper repo) : base(context)
        {
            _repo = repo;
        }

        private string UploadedFile(IFormFile attachmentFile)
        {
            string rootPath = "C:\\Users\\bobof\\Desktop\\ContactApp\\ContactApp\\wwwroot";
            string uploadFolder = Path.Combine(rootPath, "Attachments");
            string uniqueFileName = Guid.NewGuid().ToString() + "-" + attachmentFile.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                attachmentFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public APIResult<AttachmentViewModel> GetAttachmentById(int attachmentId)
        {
            APIResult<AttachmentViewModel> result = new APIResult<AttachmentViewModel>();
            AttachmentViewModel? attachmentVM = FindByCondition(x => x.Id == attachmentId)
                .Include(x => x.Creator).Include(x => x.Modifier).Include(x => x.Customer)
                .Select(x => new AttachmentViewModel(x)).FirstOrDefault();
            if (attachmentVM!=null)
                result.OKResult(200, attachmentVM);
            else
                result.ErrorResult(404, "NotFound");
            return result;
        }

        public APIResult<AttachmentViewModel> GetAttachmentsByCustomer(int customerId)
        {
            APIResult<AttachmentViewModel> result = new APIResult<AttachmentViewModel>();
            IList<AttachmentViewModel> attachmenyVM = FindByCondition(x => x.CustomerId==customerId)
                .Include(x => x.Creator).Include(x => x.Modifier).Include(x => x.Customer)
                .Select(x => new AttachmentViewModel(x)).ToList();
            if (attachmenyVM.Any())
                result.OKResult(200, attachmenyVM);
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public AttachmentViewModel? AddAttachment(int customerId)
        {
            AttachmentViewModel attachmentVM = new AttachmentViewModel();
            attachmentVM.Customer = _repo.customer.FindByCondition(x => x.Id == customerId).Select(x => new CustomerInfoViewModel(x)).FirstOrDefault();
            if (attachmentVM.Customer == null)
                return  null;
            attachmentVM.CustomerId = customerId;
            return attachmentVM;
        }

        public bool AddAttachment(AttachmentViewModel attachmentVM, int loggedUser)
        {
            if (UploadedFile(attachmentVM.AttachmentFile) != null)
            {
                CustomerAttachment newAttachment = (CustomerAttachment)attachmentVM;
                newAttachment.AttachmentUrl = UploadedFile(attachmentVM.AttachmentFile);
                newAttachment.CreatorId = newAttachment.ModifierId = loggedUser;
                newAttachment.CreationDate = newAttachment.LastModifiedDate = DateTime.Now;
                Create(newAttachment);
                _repo.Save();
                return true;
            }
            return false;
        }

        public APIResult<AttachmentViewModel> AddAttachment(IFormFile attachmentFile, int customerId, int loggedUser)
        {
            APIResult<AttachmentViewModel> result = new APIResult<AttachmentViewModel>();
            if (_repo.customer.FindByCondition(x => x.Id == customerId).Any())
            {
                CustomerAttachment attachment = new CustomerAttachment();
                attachment.AttachmentUrl = UploadedFile(attachmentFile);
                attachment.CustomerId = customerId;
                attachment.CreatorId = attachment.ModifierId = loggedUser;
                attachment.CreationDate = attachment.LastModifiedDate = DateTime.Now;
                Create(attachment);
                _repo.Save();
                AttachmentViewModel attachmentVM = FindByCondition(x => x.Id == attachment.Id)
                .Include(x => x.Creator).Include(x => x.Modifier).Include(x => x.Customer)
                .Select(x => new AttachmentViewModel(x)).First();
                result.OKResult(200, attachmentVM, "Successfully Added");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public APIResult<AttachmentViewModel> DeleteAttachment(int id, int loggedUser)
        {
            APIResult<AttachmentViewModel> result = new APIResult<AttachmentViewModel>();
            var attachment = FindByCondition(x => x.Id == id)
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .Include(x => x.Customer).FirstOrDefault();
            if (attachment!=null)
            {
                attachment.ModifierId = loggedUser;
                attachment.LastModifiedDate = DateTime.Now;
                attachment.IsDeleted = true;
                Update(attachment);
                _repo.Save();
                result.OKResult(200, new AttachmentViewModel(attachment), "Successfully Deleted");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public APIResult<AttachmentViewModel> RestoreAttachment(int id, int loggedUser)
        {
            APIResult<AttachmentViewModel> result = new APIResult<AttachmentViewModel>();
            var attachment = FindByCondition(x => x.Id == id)
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .Include(x => x.Customer).FirstOrDefault();
            if (attachment!=null)
            {
                attachment.ModifierId = loggedUser;
                attachment.LastModifiedDate = DateTime.Now;
                attachment.IsDeleted = false;
                Update(attachment);
                _repo.Save();
                result.OKResult(200, new AttachmentViewModel(attachment), "Successfully restored");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }
    }
}
