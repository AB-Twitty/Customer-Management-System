using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Contact.VM;

namespace Contact.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilterAttribute))]
    public class ContactTypeController : ApiBaseController
    {
        public ContactTypeController(IRepositoryWrapper repo) : base(repo) { }

        [HttpGet("GetType/{typeId}")]
        public APIResult<ContactTypeViewModel> GetTypeById(int typeId)
        {
            return _repo.contactType.GetTypeById(typeId);
        }

        [HttpGet("GetContactTypes")]
        public APIResult<ContactTypeViewModel> GetContactTypes()
        {
            return _repo.contactType.GetContactTypes();
        }
    }
}
