using Contact.DAL;

namespace Contact.VM
{
    public class CustomerSearchViewModel
    {
        public CustomerSearchViewModel() {
        }
        public int CurrentPageIndex { get; set; }
        public string? Name { get; set; }
        public int? StartAge { get; set; }
        public int? EndAge { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? NationalityId { get; set; }
    }
}
