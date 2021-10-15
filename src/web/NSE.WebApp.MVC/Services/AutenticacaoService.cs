using NSE.WebApp.MVC.Models.Response;
using NSE.WebApp.MVC.Models.Usuario;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContente = SerializarConteudo(usuarioLogin);
            var response = await _httpClient.PostAsync("https://localhost:44332/api/identidade/autenticar", loginContente);

            if (!TratarErroResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };                    
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContente = SerializarConteudo(usuarioRegistro);
            var response = await _httpClient.PostAsync("https://localhost:44332/api/identidade/nova-conta", registroContente);

            if (!TratarErroResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}
