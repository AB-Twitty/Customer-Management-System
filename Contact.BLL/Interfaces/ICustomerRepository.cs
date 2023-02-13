using Contact.DAL;
using Contact.VM;
using Microsoft.AspNetCore.Http;


namespace Contact.BLL.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        public bool IsCustomerExist(int id);
        public APIResult<CustomerInfoViewModel> GetCustomers(int currentPage); //API
        public CustomerSearchViewModel GetCustomers(CustomerSearchViewModel searchVM, int currentPage);
        public APIResult<CustomerInfoViewModel> SearchCustomers(CustomerSearchViewModel searchVM); //API
        public APIResult<CustomerDetailsViewModel> GetCustomerDetails(int id); //API
        public CustomerViewModel? UpdateCustomer(int id);
        public APIResult<CustomerDetailsViewModel> UpdateCustomer(CustomerAddViewModel customerVM, int loggedUser); //API
        public bool ClearImage(int id, int loggedUser);
        public APIResult<CustomerDetailsViewModel> DeleteCustomer(int id, int loggedUser); //API
        public APIResult<CustomerDetailsViewModel> RestoreCustomer(int id, int loggedUser); //API
        public CustomerViewModel AddCustomer();
        public APIResult<CustomerDetailsViewModel> AddCustomer(CustomerAddViewModel customerVM, int loggedUser); //API
        public CustomerViewModel? ContactDetails(int id);
        public CustomerViewModel? AttachmentDetails(int id);
        public bool IsNationalIdAvailable(string nationaliId);
        public APIResult<CustomerDetailsViewModel> AddImage(IFormFile ImageFile, int customerId, int loggedUser);
    }
}
