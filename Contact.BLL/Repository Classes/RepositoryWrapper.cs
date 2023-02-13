using Contact.BLL.Interfaces;
using Contact.DAL;

namespace Contact.BLL.Repository_Classes
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ContactDBContext _context;
        private ICustomerRepository _customer = null!;
        private IUserRepository _user = null!;
        private IContactRepository _contact = null!;
        private INationalityRepository _nationality = null!;
        private IContactTypeRepository _contactType = null!;
        private IAttachmentRepository _attachment = null!;
        public RepositoryWrapper(ContactDBContext context)
        {
            _context = context;
        }
        public ICustomerRepository customer { get
            {
                if (_customer == null)
                    _customer = new CustomerRepository(_context, this);
                return _customer;
            } }
        public IUserRepository user { get
            {
                if (_user == null)
                    _user = new UserRepository(_context, this);
                return _user;
            } }
        public IContactRepository contact { get
            {
                if (_contact == null)
                    _contact = new ContactRepository(_context, this);
                return _contact;
            } }
        public INationalityRepository nationality { get
            {
                if (_nationality == null)
                    _nationality = new NationalityRepository(_context, this);
                return _nationality;
            } }
        public IContactTypeRepository contactType { get
            {
                if (_contactType == null)
                    _contactType = new ContactTypeRepository(_context, this);
                return _contactType;
            } }
        public IAttachmentRepository attachment { get
            {
                if (_attachment == null)
                    _attachment = new AttachmentRepository(_context, this);
                return _attachment;
            } }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
