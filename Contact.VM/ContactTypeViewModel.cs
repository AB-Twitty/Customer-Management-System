using Contact.DAL;

namespace Contact.VM
{
    public class ContactTypeViewModel
    {
        public ContactTypeViewModel() { }
        public ContactTypeViewModel(ContactType type)
        {
            Id = type.Id;
            TypeName = type.TypeName;
            ValidationExpression = type.ValidationExpression;
        }
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string ValidationExpression { get; set; } = null!;
    }
}
