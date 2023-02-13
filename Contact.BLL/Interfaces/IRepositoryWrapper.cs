namespace Contact.BLL.Interfaces
{
    public interface IRepositoryWrapper
    {
        public ICustomerRepository customer { get; }
        public IUserRepository user { get; }
        public IContactRepository contact { get; }
        public INationalityRepository nationality { get; }
        public IContactTypeRepository contactType { get; }
        public IAttachmentRepository attachment { get; }
        public void Save();

    }
}
