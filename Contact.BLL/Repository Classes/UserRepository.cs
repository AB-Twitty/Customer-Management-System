using Contact.BLL.Interfaces;
using Contact.DAL;
using Contact.VM;

namespace Contact.BLL.Repository_Classes
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {
        private readonly IRepositoryWrapper _repo;
        public UserRepository(ContactDBContext context, IRepositoryWrapper repo) : base(context)
        {
            _repo = repo;
        }

        public APIResult<UserInfoViewModel> Register(UserViewModel userVM)
        {
            APIResult<UserInfoViewModel> result = new APIResult<UserInfoViewModel>();
            User user = new User();
            user = (User)userVM;
            Create(user);
            _repo.Save();
            result.OKResult(200, new UserInfoViewModel(user), "Successfully Registered");
            return result;
        }

        public APIResult<UserInfoViewModel> Login(LoginViewModel logVM)
        {
            APIResult<UserInfoViewModel> result = new APIResult<UserInfoViewModel>();
            UserInfoViewModel? user = FindByCondition(x =>
                (x.Username == logVM.UsernameOrEmail || x.Email == logVM.UsernameOrEmail)
                && x.Password == logVM.Password).Select(x => new UserInfoViewModel(x)).FirstOrDefault();
            if (user == null)
                result.ErrorResult(404, "Not Found");
            else
                result.OKResult(200, user);
            return result;
        }

        public APIResult<Boolean> Logout()
        {
            APIResult<Boolean> result = new APIResult<Boolean>();
            result.OKResult(200, true, "Successfully Loggedout");
            return result;
        }

        /*public bool ChangePassword(UserViewModel userVM)
        {
            var user = _context.Users.Find(userVM.UserId);
            if (user != null && userVM.OldPassword == userVM.Password)
            {
                Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }*/

        public APIResult<UserInfoViewModel> UpdateUser(UserInfoViewModel userVM)
        {
            APIResult<UserInfoViewModel> result = new APIResult<UserInfoViewModel>();
            var user = _context.Users.Find(userVM.userId);
            if (user != null)
            {
                user.Username = userVM.username;
                user.Email = userVM.email;
                user.Name = userVM.name;
                _repo.Save();
                result.OKResult(200, new UserInfoViewModel(user), "Successfully Updated");
            }
            else
                result.ErrorResult(404, "Not Found");
            return result;
        }
    }
}
