using Contact.BLL.Interfaces;
using Contact.DAL;
using Contact.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Contact.BLL.Repository_Classes
{
    internal class CustomerRepository : RepositoryBase<Customer> , ICustomerRepository
    {
        private readonly IRepositoryWrapper _repo;
        public CustomerRepository(ContactDBContext context, IRepositoryWrapper repo) : base(context)
        {
            _repo = repo;
        }

        private string UploadedFile(IFormFile ImageFile)
        {
            //C:\Users\bobof\Desktop\ContactApp\ContactApp\wwwroot
            string uniqueFileName;
            if (ImageFile != null)
            {
                string rootPath = "C:\\Users\\bobof\\Desktop\\ContactApp\\contact-app\\src\\assets";
                string uploadFolder = Path.Combine(rootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "-" + ImageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "default.png";
            }
            return uniqueFileName;
        }
        
        public APIResult<CustomerInfoViewModel> GetCustomers(int currentPage)
        {
            currentPage = currentPage <= 0 ? 1 : currentPage;
            APIResult<CustomerInfoViewModel> result = new APIResult<CustomerInfoViewModel>();
            const int PAGESIZE = 8;
            IList<CustomerInfoViewModel> customers = FindAll().Select(x => new CustomerInfoViewModel(x)).ToList();
            result.PageCountCalc(customers.Count(), PAGESIZE);
            if (currentPage <= result.TotalCount)
            {
                customers = customers.Skip((currentPage - 1) * PAGESIZE).Take(PAGESIZE).ToList();
                result.OKResult(200, customers);
            }
            else
                result.ErrorResult(400, "Bad Request");
            return result;
        }

        public CustomerSearchViewModel GetCustomers(CustomerSearchViewModel searchVM, int currentPage)
        {
            const int PAGESIZE = 2;
            CustomerSearchViewModel customersVM = new CustomerSearchViewModel();
            var customers = FindAll();
            if (!string.IsNullOrWhiteSpace(searchVM.Name))
                customers = customers.Where(x => (x.FirstName + " " + x.LastName).Contains(searchVM.Name));
            if (searchVM.StartAge != null)
            {
                DateTime today = DateTime.Now;
                DateTime startDate = new DateTime((int)(today.Year - searchVM.StartAge), today.Month, today.Day);
                customers = customers.Where(x => x.BirthDate <= startDate);
            }
            if (searchVM.EndAge != null)
            {
                DateTime today = DateTime.Now;
                DateTime endDate = new DateTime((int)(today.Year - searchVM.EndAge), today.Month, today.Day);
                customers = customers.Where(x => x.BirthDate >= endDate);
            }
            if (searchVM.NationalityId != null)
                customers = customers.Where(x => x.NationalityId == searchVM.NationalityId);
            if (searchVM.IsActive == true)
                customers = customers.Where(x => x.IsActive == searchVM.IsActive);
            if (searchVM.IsDeleted == true)
                customers = customers.Where(x => x.IsDeleted == searchVM.IsDeleted);
            double pageCount = (double)(customers.Count() / Convert.ToDecimal(PAGESIZE));
            //customersVM.PageCount = (int)Math.Ceiling(pageCount);
            customersVM.CurrentPageIndex = currentPage;
            customers = customers.Skip((currentPage - 1) * PAGESIZE).Take(PAGESIZE);
            /*foreach (var customer in customers)
                customersVM.Customers.Add(new CustomerInfoViewModel(customer));
            customersVM.Nationalities = _repo.nationality.FindAll().Select(x => new NationalityViewModel(x)).ToList();*/
            return customersVM;
        }

        public APIResult<CustomerInfoViewModel> SearchCustomers(CustomerSearchViewModel searchVM)
        {
            searchVM.CurrentPageIndex = searchVM.CurrentPageIndex <= 0 ? 1 : searchVM.CurrentPageIndex;
            APIResult<CustomerInfoViewModel> result = new APIResult<CustomerInfoViewModel>();
            const int PAGESIZE = 4;

            DateTime today = DateTime.Now;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if(searchVM.StartAge!=null)
                startDate = new DateTime((int)(today.Year - searchVM.StartAge), today.Month, today.Day);
            if(searchVM.EndAge!=null)
                endDate = new DateTime((int)(today.Year - searchVM.EndAge), today.Month, today.Day);

            var customers = FindByCondition(x => 
            (string.IsNullOrWhiteSpace(searchVM.Name) || x.FirstName.Contains(searchVM.Name) || x.LastName.Contains(searchVM.Name)) &&
            (searchVM.StartAge == null || x.BirthDate <= startDate) &&
            (searchVM.EndAge == null || x.BirthDate >= endDate) &&
            (searchVM.NationalityId == null || searchVM.NationalityId == x.NationalityId) &&
            (searchVM.IsActive == null || searchVM.IsActive == x.IsActive) &&
            (searchVM.IsDeleted == null || searchVM.IsDeleted == x.IsDeleted)).Select(c => new CustomerInfoViewModel(c)).ToList();
            result.PageCountCalc(customers.Count(), PAGESIZE);
            searchVM.CurrentPageIndex = searchVM.CurrentPageIndex>result.TotalCount ? 1 : searchVM.CurrentPageIndex;
            customers = customers.Skip((searchVM.CurrentPageIndex - 1) * PAGESIZE).Take(PAGESIZE).ToList();
            result.OKResult(200, customers);
            return result;
        }

        public APIResult<CustomerDetailsViewModel> GetCustomerDetails(int id)
        {
            APIResult<CustomerDetailsViewModel> result = new APIResult<CustomerDetailsViewModel>();
            var customer = FindByCondition(customer => customer.Id == id)
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .Include(x => x.Nationality).FirstOrDefault();
            if (customer != null)
                result.OKResult(200, new CustomerDetailsViewModel(customer));
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public CustomerViewModel? UpdateCustomer(int id)
        {
            var customer = FindByCondition(customer => customer.Id == id).FirstOrDefault();
            if (customer != null)
            {
                CustomerViewModel customerVM = new CustomerViewModel(customer);
                customerVM.Nationalities = _repo.nationality.FindAll().Select(x => new NationalityViewModel(x)).ToList();
                return customerVM;
            }
            return null;
        }

        public APIResult<CustomerDetailsViewModel> UpdateCustomer(CustomerAddViewModel customerVM, int loggedUser)
        {
            APIResult<CustomerDetailsViewModel> result = new APIResult<CustomerDetailsViewModel>();
            var customer = FindByCondition(x => x.Id == customerVM.id).FirstOrDefault();
            if (customer != null)
            {
                customerVM.copyTo(customer);
                customer.LastModifiedDate = DateTime.Now;
                customer.ModifierId = loggedUser;
                Update(customer);
                _repo.Save();
                result.OKResult(200, GetCustomerDetails(customer.Id).Data, "Successfully Updated");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public bool ClearImage(int id, int loggedUser)
        {
            var customer = FindByCondition(x => x.Id == id).FirstOrDefault();
            if (customer == null)
                return false;
            if (customer.CustomerImageUrl != null && customer.CustomerImageUrl != "default.png")
            {
                string rootPath = "C:\\Users\\bobof\\Desktop\\ContactApp\\ContactApp\\wwwroot";
                var imagePath = Path.Combine(rootPath, "Images", customer.CustomerImageUrl);
                System.IO.File.Delete(imagePath);
            }
            customer.CustomerImageUrl = "default.png";
            customer.ModifierId = loggedUser;
            customer.LastModifiedDate = DateTime.Now;
            Update(customer);
            _repo.Save();
            return true;
        }

        public APIResult<CustomerDetailsViewModel> DeleteCustomer(int id, int loggedUser)
        {
            APIResult<CustomerDetailsViewModel> result = new APIResult<CustomerDetailsViewModel>();
            var customer = FindByCondition(x => x.Id == id).FirstOrDefault();
            if (customer!=null)
            {
                customer.IsDeleted = true;
                customer.IsActive = false;
                customer.LastModifiedDate = DateTime.Now;
                customer.ModifierId = loggedUser;
                Update(customer);
                _repo.Save();
                result.OKResult(200, GetCustomerDetails(customer.Id).Data, "Successfully Deleted");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public APIResult<CustomerDetailsViewModel> RestoreCustomer(int id, int loggedUser)
        {
            APIResult<CustomerDetailsViewModel> result = new APIResult<CustomerDetailsViewModel>();
            var customer = FindByCondition(x => x.Id == id).FirstOrDefault();
            if (customer!=null)
            {
                customer.IsDeleted = false;
                customer.LastModifiedDate = DateTime.Now;
                customer.ModifierId = loggedUser;
                Update(customer);
                _repo.Save();
                result.OKResult(200, GetCustomerDetails(customer.Id).Data, "Successfully Restored");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }

        public CustomerViewModel AddCustomer()
        {
            CustomerViewModel customerVM = new CustomerViewModel();
            customerVM.Nationalities = _repo.nationality.FindAll().Select(x => new NationalityViewModel(x)).ToList();
            return customerVM;
        }

        public APIResult<CustomerDetailsViewModel> AddCustomer(CustomerAddViewModel customerVM, int loggedUser)
        {
            APIResult<CustomerDetailsViewModel> result = new APIResult<CustomerDetailsViewModel>();
            Customer newCustomer = (Customer)customerVM;
            newCustomer.CustomerImageUrl = UploadedFile(customerVM.imageFile);
            newCustomer.CreatorId = newCustomer.ModifierId = loggedUser;
            newCustomer.CreationDate = newCustomer.LastModifiedDate = DateTime.Now;
            Create(newCustomer);
            _repo.Save();
            result.OKResult(200, GetCustomerDetails(newCustomer.Id).Data, "Successfully Added");
            return result;
        }

        public CustomerViewModel? ContactDetails(int id)
        {
            var customer = FindByCondition(x => x.Id == id)
                .Include(x => x.CustomerContacts).ThenInclude(x => x.Creator)
                .Include(x => x.CustomerContacts).ThenInclude(x => x.Modifier)
                .Include(x => x.CustomerContacts).ThenInclude(x => x.ContactType)
                .FirstOrDefault();
            if (customer == null)
                return null;
            return new CustomerViewModel(customer);
        }

        public CustomerViewModel? AttachmentDetails(int id)
        {
            var customer = FindByCondition(x => x.Id == id)
                .Include(x => x.CustomerAttachments).ThenInclude(x => x.Creator)
                .Include(x => x.CustomerAttachments).ThenInclude(x => x.Modifier)
                .FirstOrDefault();
            if (customer == null)
                return null;
            return new CustomerViewModel(customer);
        }

        public bool IsNationalIdAvailable(string nationalId)
        {
            return FindAll().Any(x => x.NationalId == nationalId);
        }

        public bool IsCustomerExist(int id)
        {
            return FindByCondition(x => x.Id == id).Any();
        }

        public APIResult<CustomerDetailsViewModel> AddImage(IFormFile ImageFile, int customerId, int loggedUser)
        {
            APIResult<CustomerDetailsViewModel> result = new APIResult<CustomerDetailsViewModel>();
            if (IsCustomerExist(customerId))
            {
                Customer customer = FindByCondition(x => x.Id == customerId).First();
                customer.CustomerImageUrl = UploadedFile(ImageFile);
                Update(customer);
                _repo.Save();
                CustomerDetailsViewModel customerVM = FindByCondition(x => x.Id==customer.Id)
                .Include(x => x.Creator)
                .Include(x => x.Modifier)
                .Include(x => x.Nationality).Select(x => new CustomerDetailsViewModel(x)).First();
                result.OKResult(200, customerVM, "Successfully Added");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }
    }
}
