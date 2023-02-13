using Contact.BLL.Interfaces;
using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Repository_Classes
{
    internal class ContactTypeRepository : RepositoryBase<ContactType>, IContactTypeRepository
    {
        private readonly IRepositoryWrapper _repo;
        public ContactTypeRepository(ContactDBContext context, IRepositoryWrapper repo) : base(context)
        {
            _repo = repo;
        }

        public APIResult<ContactTypeViewModel> GetTypeById(int typeId)
        {
            APIResult<ContactTypeViewModel> result = new APIResult<ContactTypeViewModel>();
            ContactTypeViewModel? typeVM = FindByCondition(x => x.Id == typeId).Select(x => new ContactTypeViewModel(x)).FirstOrDefault();
            if (typeVM == null)
                result.ErrorResult(404, "Not Found");
            else
                result.OKResult(200, typeVM);
            return result;
        }

        public APIResult<ContactTypeViewModel> GetContactTypes()
        {
            APIResult<ContactTypeViewModel> result = new APIResult<ContactTypeViewModel>();
            IList<ContactTypeViewModel> types = FindAll().Select(x => new ContactTypeViewModel(x)).ToList();
            result.OKResult(200, types);
            return result;
        }
    }
}
