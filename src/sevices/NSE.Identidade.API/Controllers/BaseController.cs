
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected List<string> Erros = new List<string>();
        public ActionResult CustomResponse(object result = null)
        {

            if (OperacaoValida())
            {
                return Ok(result);
            }


            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            List<ModelError> erros = modelState.Values.SelectMany(sm => sm.Errors).ToList();

            erros.ForEach(erro =>
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            });

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
