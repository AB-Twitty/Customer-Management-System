using Contact.DAL;

namespace Contact.VM
{
    public class CustomerDetailsViewModel
    {
        public CustomerDetailsViewModel() { }
        public CustomerDetailsViewModel(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            NationalId = customer.NationalId;
            BirthDate = customer.BirthDate;
            IsActive = customer.IsActive;
            IsDeleted = (bool)customer.IsDeleted;
            CustomerImageURL = customer.CustomerImageUrl;
            CreationDate = customer.CreationDate;
            LastModifiedDate = customer.LastModifiedDate;
            Creator = new UserInfoViewModel(customer.Creator);
            Modifier = new UserInfoViewModel(customer.Modifier);
            Nationality = new NationalityViewModel(customer.Nationality);
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CustomerImageURL { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public virtual UserInfoViewModel Creator { get; set; }
        public virtual UserInfoViewModel Modifier { get; set; }
        public virtual NationalityViewModel Nationality { get; set; }
    }
}
