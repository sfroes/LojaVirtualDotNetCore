using NSE.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        /// <summary>
        /// Serializar conteudo
        /// </summary>
        /// <param name="dado">Objeto a ser serializado</param>
        /// <returns>Uma StringContent com objeto serializado</returns>
        protected StringContent SerializarConteudo(object dado)
        {
            return new StringContent(JsonSerializer.Serialize(dado), Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Deserializar um response
        /// </summary>
        /// <typeparam name="T">Objeto de resposta</typeparam>
        /// <param name="httpResponseMessage">Response a ser deserializado</param>
        /// <returns>Objeto do tipo T deserializado</returns>
        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), options)
        }

        protected bool TratarErroResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
