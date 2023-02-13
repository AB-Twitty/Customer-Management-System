using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Contact.VM;

namespace Contact.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilterAttribute))]
    public class NationalityController : ApiBaseController
    {
        public NationalityController(IRepositoryWrapper repo) : base(repo) { }

        [HttpGet("GetNationality/{nationalityId}")]
        public APIResult<NationalityViewModel> GetNationalityById(int nationalityId)
        {
            return _repo.nationality.GetNationalityById(nationalityId);
        }

        [HttpGet("NationalitiesList/{isActive}")]
        public APIResult<NationalityViewModel> GetNationalities(bool isActive)
        {
            return _repo.nationality.GetNationalities(isActive);
        }
    }
}
