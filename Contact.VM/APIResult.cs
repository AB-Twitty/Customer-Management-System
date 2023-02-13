using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contact.VM
{
    public class APIResult<T>
    {
        public int Status { get; set; }
        public int TotalCount { get; set; }
        public T Data { get; set; }
        public IList<T> DataList { get; set; }
        public string RespondMessage { get; set; }
        public IList<ErrorObj> ErrorMessages { get; set; }
        public void OKResult(int status, T data, string respondMessage= "The request has succeeded")
        {
            Status = status;
            Data = data;
            RespondMessage = respondMessage;
        }
        public void OKResult(int status, IList<T> list, string respondMessage= "The request has succeeded")
        {
            Status = status;
            DataList = list;
            RespondMessage = respondMessage;
        }
        public void ErrorResult(int status, string respondMessage)
        {
            Status = status;
            RespondMessage = respondMessage;
        }
        public void ErrorResult(int status, string respondMessage, IList<ErrorObj> errors)
        {
            Status = status;
            RespondMessage = respondMessage;
            ErrorMessages = errors;
        }
        public void PageCountCalc(int count, int size)
        {
            double pageCount = (double)(count / Convert.ToDecimal(size));
            TotalCount = (int)Math.Ceiling(pageCount);
        }
    }
}
