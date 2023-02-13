using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Contact.VM;

namespace Contact.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilterAttribute))]
    public class ContactController : ApiBaseController
    {
        public ContactController(IRepositoryWrapper repo) : base(repo) { }
        
        [HttpGet("GetContact/{contactId}")]
        public APIResult<ContactViewModel> GetContactByID(int contactId)
        {
            return _repo.contact.GetContactById(contactId);
        }

        [HttpGet("GetContactsFor/{customerId}")]
        public APIResult<ContactViewModel> GetConatctsByCustomer(int customerId)
        {
            return _repo.contact.GetContactsByCustomer(customerId);
        }

        [HttpPost("AddContactTo/{customerId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<ContactViewModel> AddContact([FromBody]ContactAddViewModel contactVM, int customerId)
        {
            return _repo.contact.AddContact(contactVM, customerId, _loggedUser);
        }

        [HttpPut("UpdateContact")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<ContactViewModel> UpdateContact([FromBody]ContactAddViewModel contactVM)
        {
            return _repo.contact.UpdateContact(contactVM, _loggedUser);
        }

        [HttpGet("DeleteContact/{contactId}")]
        public APIResult<ContactViewModel> DeleteContact(int contactId)
        {
            return _repo.contact.DeleteContact(contactId, _loggedUser);
        }

        [HttpGet("RestoreContact/{contactId}")]
        public APIResult<ContactViewModel> RestoreContact(int contactId)
        {
            return _repo.contact.RestoreContact(contactId, _loggedUser);
        }
    }
}
