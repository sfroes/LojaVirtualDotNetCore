using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Usuario;
using NSE.WebApp.MVC.Services;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
        private readonly IAutenticacaoService _autentiicacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            _autentiicacaoService = autenticacaoService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioRegistro);
            }

            // Api - Registro
            string resposta = await _autentiicacaoService.Registro(usuarioRegistro);


            if (false) return View(usuarioRegistro);

            //Realizar Login na app

            return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioLogin);
            }

            // Api - Login
            string resposta = await _autentiicacaoService.Login(usuarioLogin);

            if (false) return View(usuarioLogin);

            //Realizar Login na app

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");

        }

    }
}
