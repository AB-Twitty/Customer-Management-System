using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Interfaces
{
    public interface INationalityRepository : IRepositoryBase<Nationality>
    {
        public APIResult<NationalityViewModel> GetNationalityById(int nationalityId);
        public APIResult<NationalityViewModel> GetNationalities(bool isActive);
    }
}
