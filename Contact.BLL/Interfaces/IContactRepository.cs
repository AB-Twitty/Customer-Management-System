using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Interfaces
{
    public interface IContactRepository : IRepositoryBase<CustomerContact>
    {
        public APIResult<ContactViewModel> GetContactById(int id); //API
        public APIResult<ContactViewModel> GetContactsByCustomer(int customerId); //API
        //public ContactViewModel? AddContact(int CustomerId);
        public void AddContact(ContactViewModel contactVM, int loggedUser);
        public APIResult<ContactViewModel> AddContact(ContactAddViewModel contactVM, int customerId, int loggedUser); //API
        public ContactViewModel? UpdateContact(int id);
        public APIResult<ContactViewModel> UpdateContact(ContactAddViewModel contactVM, int loggedUser); //API
        public APIResult<ContactViewModel> DeleteContact(int id, int loggedUser); //API
        public APIResult<ContactViewModel> RestoreContact(int id, int loggedUser); //API
    }
}
