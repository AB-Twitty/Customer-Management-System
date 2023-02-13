using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Contact.VM;

namespace Contact.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilterAttribute))]
    public class CustomerController : ApiBaseController
    {
        public CustomerController(IRepositoryWrapper repo) : base(repo) { }

        [HttpGet("GetAllCustomers/{currentPage}")]
        public APIResult<CustomerInfoViewModel> GetCustomers(int currentPage)
        {
            return _repo.customer.GetCustomers(currentPage);
        }

        [HttpPost("SearchCustomers")]
        public APIResult<CustomerInfoViewModel> GetCustomers(CustomerSearchViewModel searchVM)
        {            
            return  _repo.customer.SearchCustomers(searchVM);
        }

        [HttpGet("CustomerDetails/{customerId}")]
        public APIResult<CustomerDetailsViewModel> GetCustomerDetails(int customerId)
        {
            return _repo.customer.GetCustomerDetails(customerId);
        }

        [HttpPost("AddCustomer")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<CustomerDetailsViewModel> AddCustomer([FromForm]CustomerAddViewModel customerVM)
        {
            return _repo.customer.AddCustomer(customerVM, _loggedUser);
        }

        [HttpPut("UpdateCustomer")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<CustomerDetailsViewModel> UpdateCustomer(CustomerAddViewModel customerVM)
        {
            return _repo.customer.UpdateCustomer(customerVM, _loggedUser);
        }

        [HttpGet("DeleteCustomer/{customerId}")]
        public APIResult<CustomerDetailsViewModel> DeleteCustomer(int customerId)
        {
            return _repo.customer.DeleteCustomer(customerId, _loggedUser);
        }

        [HttpGet("RestoreCustomer/{customerId}")]
        public APIResult<CustomerDetailsViewModel> RestoreCustomer(int customerId)
        {
            return _repo.customer.RestoreCustomer(customerId, _loggedUser);
        }

        [HttpPost("AddCustomerImage/{customerId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<CustomerDetailsViewModel> AddImage(IFormFile ImageFile, int customerId)
        {
            return _repo.customer.AddImage(ImageFile, customerId, _loggedUser);
        }
    }
}
