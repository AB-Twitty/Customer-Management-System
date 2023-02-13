using Contact.BLL.Interfaces;
using Contact.DAL;
using Contact.VM;
using Microsoft.EntityFrameworkCore;

namespace Contact.BLL.Repository_Classes
{
    internal class ContactRepository : RepositoryBase<CustomerContact>, IContactRepository
    {
        private readonly IRepositoryWrapper _repo;
        public ContactRepository(ContactDBContext context, IRepositoryWrapper repo) : base(context)
        {
            _repo = repo;
        }

        public APIResult<ContactViewModel> GetContactById(int id)
        {
            APIResult<ContactViewModel> result = new APIResult<ContactViewModel>();
            ContactViewModel? contactVM = FindByCondition(x => x.Id == id)
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .Include(x => x.ContactType)
                .Select(x => new ContactViewModel(x)).FirstOrDefault();
            if (contactVM == null)
                result.ErrorResult(404, "Not Found");
            else
                result.OKResult(200, contactVM);
            return result;
        }

        public APIResult<ContactViewModel> GetContactsByCustomer(int customerId)
        {
            APIResult<ContactViewModel> result = new APIResult<ContactViewModel>();
            IList<ContactViewModel> contactVM = FindByCondition(x => x.CustomerId == customerId)
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .Include(x => x.ContactType).Select(x => new ContactViewModel(x)).ToList();
            if (!contactVM.Any())
                result.ErrorResult(404, "Not Found");
            else
                result.OKResult(200, contactVM);
            return result;
        }

        /*public ContactViewModel? AddContact(int customerId)
        {
            ContactViewModel contactVM = new ContactViewModel();
            contactVM.Customer = _repo.customer.FindByCondition(x => x.Id == customerId).Select(x => new CustomerInfoViewModel(x)).FirstOrDefault();
            if (contactVM.Customer == null)
                return null;
            contactVM.CustomerId = customerId;
            //contactVM.ContactTypes = _repo.contactType.FindAll().ToList();
            return contactVM;
        }*/

        public void AddContact(ContactViewModel contactVM, int loggedUser)
        {
            CustomerContact contact = new CustomerContact();
            contact = (CustomerContact)contactVM;
            contact.CreatorId = contact.ModifierId = loggedUser;
            contact.CreationDate = contact.LastModifiedDate = DateTime.Now;
            Create(contact);
            _repo.Save();
        }

        public APIResult<ContactViewModel> AddContact(ContactAddViewModel newContact, int customerId, int loggedUser)
        {
            APIResult<ContactViewModel> result = new APIResult<ContactViewModel>();
            if(_repo.customer.FindByCondition(x => x.Id==customerId).Any() && newContact.customerId==customerId)
            {
                CustomerContact contact = new CustomerContact();
                contact = (CustomerContact)newContact;
                contact.IsDeleted = false;
                contact.CreatorId = contact.ModifierId = loggedUser;
                contact.CreationDate = contact.LastModifiedDate = DateTime.Now;
                Create(contact);
                _repo.Save();
                result.OKResult(200, GetContactById(contact.Id).Data, "Successfully Added");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public ContactViewModel? UpdateContact(int id)
        {
            var contact = FindByCondition(x => x.Id == id).FirstOrDefault();
            if (contact == null)
                return null;
            ContactViewModel contactVM = new ContactViewModel(contact);
            //contactVM.ContactTypes = _repo.contactType.FindAll().ToList();
            return contactVM;
        }

        public APIResult<ContactViewModel> UpdateContact(ContactAddViewModel contactVM, int loggedUser)
        {
            APIResult<ContactViewModel> result = new APIResult<ContactViewModel>();
            var contact = FindByCondition(x => x.Id == contactVM.id).FirstOrDefault();
            if (contact!=null)
            {
                contact.Contact = contactVM.contact;
                contact.IsActive = contactVM.isActive;
                contact.ModifierId = loggedUser;
                contact.LastModifiedDate = DateTime.Now;
                Update(contact);
                _repo.Save();
                result.OKResult(200, GetContactById(contact.Id).Data, "Successfully Updated");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public APIResult<ContactViewModel> DeleteContact(int id, int loggedUser)
        {
            APIResult<ContactViewModel> result = new APIResult<ContactViewModel>();
            var contact = FindByCondition(x => x.Id == id).FirstOrDefault();
            if (contact!=null)
            {
                contact.ModifierId = loggedUser;
                contact.LastModifiedDate = DateTime.Now;
                contact.IsActive = false;
                contact.IsDeleted = true;
                Update(contact);
                _repo.Save();
                result.OKResult(200, GetContactById(contact.Id).Data, "Successfully Deleted");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public APIResult<ContactViewModel> RestoreContact(int id, int loggedUser)
        {
            APIResult<ContactViewModel> result = new APIResult<ContactViewModel>();
            var contact = FindByCondition(x => x.Id == id).FirstOrDefault();
            if (contact != null)
            {
                contact.ModifierId = loggedUser;
                contact.LastModifiedDate = DateTime.Now;
                contact.IsDeleted = false;
                Update(contact);
                _repo.Save();
                result.OKResult(200, GetContactById(contact.Id).Data, "Successfully Restored");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }
    }
}
