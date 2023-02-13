using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Contact.VM;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IRepositoryWrapper _repo;
        protected static int _loggedUser = 0;
        public ApiBaseController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        protected APIResult<UserInfoViewModel> SetLoggedUser(APIResult<UserInfoViewModel> result)
        {
            if(result.Data!=null)
                _loggedUser = result.Data.userId;
            return result;
        }

        protected APIResult<Boolean> ClearLoggedUser(APIResult<Boolean> result)
        {
            _loggedUser = 0;
            return result;
        }

        public class ValidationFilterAttribute : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    APIResult<string> result = new APIResult<string>();
                    IList<ErrorObj> errorList = new List<ErrorObj>();
                    foreach (var model in context.ModelState)
                    {
                        if (model.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            errorList.Add(new ErrorObj
                            {
                                Prop = model.Key,
                                Errors = model.Value.Errors.Select(x => x.ErrorMessage).ToList()
                            });
                    }
                    result.ErrorResult(422, "Unaccessable Entity", errorList);
                    context.Result = new ObjectResult(result);
                }
            }
            public void OnActionExecuted(ActionExecutedContext context) { }
        }

        public class AuthorizationFilterAttribute : IActionFilter
        {
            private readonly IRepositoryWrapper _repo;
            public AuthorizationFilterAttribute(IRepositoryWrapper repo)
            {
                _repo = repo;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (!_repo.user.FindByCondition(x => x.Id==_loggedUser).Any())
                {
                    APIResult<string> result = new APIResult<string>();
                    result.ErrorResult(401, "Unauthorized");
                    context.Result = new ObjectResult(result);
                }
            }
            public void OnActionExecuted(ActionExecutedContext context) { }
        }
    }
}
