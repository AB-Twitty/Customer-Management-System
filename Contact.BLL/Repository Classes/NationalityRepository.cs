using Contact.BLL.Interfaces;
using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Repository_Classes
{
    internal class NationalityRepository : RepositoryBase<Nationality>, INationalityRepository
    {
        private readonly IRepositoryWrapper _repo;
        public NationalityRepository(ContactDBContext context, IRepositoryWrapper repo) : base(context)
        {
            _repo = repo;
        }

        public APIResult<NationalityViewModel> GetNationalityById(int nationalityId)
        {
            APIResult<NationalityViewModel> result = new APIResult<NationalityViewModel>();
            NationalityViewModel? nationalityVM = FindByCondition(x => x.Id == nationalityId).Select(x => new NationalityViewModel(x)).FirstOrDefault();
            if (nationalityVM == null)
                result.ErrorResult(404, "Not Found");
            else 
                result.OKResult(200, nationalityVM);
            return result;
        }

        public APIResult<NationalityViewModel> GetNationalities(bool isActive)
        {
            APIResult<NationalityViewModel> result = new APIResult<NationalityViewModel>();
            IList<NationalityViewModel> nationalities = FindByCondition(x => x.IsActive == isActive && x.IsActive)
                .Select(x => new NationalityViewModel(x)).ToList();
            result.OKResult(200, nationalities);
            return result;
        }
    }
}
