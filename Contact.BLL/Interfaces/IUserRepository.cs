using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public APIResult<UserInfoViewModel> Register(UserViewModel userVM); //API
        public APIResult<UserInfoViewModel> Login(LoginViewModel logVM); //API
        public APIResult<Boolean> Logout();
        //public bool ChangePassword(UserViewModel userVM); 
        public APIResult<UserInfoViewModel> UpdateUser(UserInfoViewModel userVM); //API
    }
}
