using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Response;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult responseResult)
        {
            if(responseResult != null && responseResult.Errors.Mensagens.Any())
            {
                return true;
            }

            return false;
        }
    }
}
