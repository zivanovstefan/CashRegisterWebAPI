using System.Net;
namespace CashRegister.Application.ErrorModels
{
    public class ErrorResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
