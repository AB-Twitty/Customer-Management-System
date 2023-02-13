using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Interfaces
{
    public interface IContactTypeRepository : IRepositoryBase<ContactType>
    {
        public APIResult<ContactTypeViewModel> GetTypeById(int typeId);
        public APIResult<ContactTypeViewModel> GetContactTypes();
    }
}
