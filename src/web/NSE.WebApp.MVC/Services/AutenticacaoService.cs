using NSE.WebApp.MVC.Models.Usuario;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(UsuarioLogin usuarioLogin)
        {
            var loginContente = new StringContent(JsonSerializer.Serialize(usuarioLogin),Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44332/api/identidade/autenticar", loginContente);

            var teste = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContente = new StringContent(JsonSerializer.Serialize(usuarioRegistro), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44332/api/identidade/nova-conta", registroContente);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
