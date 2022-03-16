using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using _4kTiles_Frontend.DataObjects;
using _4kTiles_Frontend.MVVM.Models.Account;
using _4kTiles_Frontend.MVVM.Models.Auth;
using _4kTiles_Frontend.Services.Configuration;

namespace _4kTiles_Frontend.Services.ApiClient
{
    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE,
    }

    // Client interface
    public interface IFkTilesClient
    {
        bool IsUserLoggedIn();

        Task<bool> Login(LoginModel dao);

        Task<Response<T?>> RequestAsync<T>(string endpoint, RequestMethod? method = RequestMethod.GET, object body = null);

        string GetJsonString<T>(T obj);
        T GetJsonObject<T>(string jsonString);

        AccountModel GetAccount();
    }

    public class FkTilesClient : IFkTilesClient
    {
        public static IFkTilesClient Client { get; private set; }

        static FkTilesClient()
        {
            Client = new FkTilesClient();
        }

        // Private fields
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly HttpClient _client;
        private string? _jwtToken = "";
        private AccountModel? _account = null;

        // Constructor
        private FkTilesClient()
        {
            // Init json serializer options
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            // Init HttpClient
            _client = new();

            // Init Base Url for client
            _client.BaseAddress = new(ConfigService.Configuration.GetSection("BaseUrl:FkTiles").Value);
        }

        // Check if user is logged in or not
        public bool IsUserLoggedIn()
        {
            // Token or data not exist ~ not logged in
            if (string.IsNullOrWhiteSpace(_jwtToken) || _account == null)
                return false;

            return true;
        }

        // Login for token
        public async Task<bool> Login(LoginModel? dao)
        {
            // Return if user logged in or input empty
            if (IsUserLoggedIn() || dao == null)
            {
                return true;
            }

            // Try to login with provided input
            var loginResponse = await RequestAsync<string>("Account/Login", RequestMethod.POST, dao);

            if (loginResponse == null)
                return false;

            _jwtToken = loginResponse.Data;

            var accountResponse = await RequestAsync<AccountModel>("Account/Account", RequestMethod.GET);

            if (accountResponse == null)
                return false;

            _account = accountResponse.Data;

            return true;
        }

        public AccountModel GetAccount()
        {
            return _account;
        }

        // Request method
        public async Task<Response<T?>> RequestAsync<T>(string endpoint, RequestMethod? method = RequestMethod.GET, object? body = null)
        {
            if (!string.IsNullOrWhiteSpace(_jwtToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

            HttpResponseMessage response = new();

            if (body == null)
            {
                response = method switch
                {
                    RequestMethod.GET => await _client.GetAsync(endpoint),
                    RequestMethod.DELETE => await _client.DeleteAsync(endpoint),
                    _ => throw new System.Exception("Invalid request method"),
                };
            }

            if (body != null)
            {
                StringContent stringContent = new StringContent(
                    JsonSerializer.Serialize(body, _jsonSerializerOptions),
                    Encoding.UTF8,
                    System.Net.Mime.MediaTypeNames.Application.Json);

                response = method switch
                {
                    RequestMethod.POST => await _client.PostAsync(endpoint, stringContent),
                    RequestMethod.PUT => await _client.PutAsync(endpoint, stringContent),
                    RequestMethod.PATCH => await _client.PatchAsync(endpoint, stringContent),
                    _ => throw new System.Exception("Invalid request method"),
                };
            }

            // get content from response
            var content = await response.Content.ReadAsStringAsync();

            // deserialize content to object
            return JsonSerializer.Deserialize<Response<T?>>(content, _jsonSerializerOptions);
        }

        public string GetJsonString<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _jsonSerializerOptions);
        }

        public T GetJsonObject<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString, _jsonSerializerOptions);
        }
    }
}
