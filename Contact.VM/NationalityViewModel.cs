using Contact.DAL;

namespace Contact.VM
{
    public class NationalityViewModel
    {
        public NationalityViewModel() { }
        public NationalityViewModel(Nationality nationality)
        {
            Id = nationality.Id;
            NationalityName = nationality.NationalityName;
            IsActive = nationality.IsActive;
        }
        public int Id { get; set; }
        public string NationalityName { get; set; }
        public bool IsActive { get; set; }
    }
}
