using IntegraCTE.Infra.Services.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegraCTE.Infra.Services
{
    public class ODataJson
    {
        private readonly ILogger<ODataJson> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ODataJson(IConfiguration configuration, HttpClient httpClient, ILogger<ODataJson> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> Lookup(string Entity, string param)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using (var httpClient = await configureClient())
            {
                try
                {
                    string str = await _httpClient.GetStringAsync($"data/{Entity}?{param}");
                    return str;
                }
                catch (Exception ex)
                {
                    //this._Validation.Add("Erro na execução do oData: " + ex.Message);
                    return null;
                }
            }
        }

        public async Task<T> Lookup<T>(string Entity, string param)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            try
            {
                using var httpClient = await configureClient();
                var str = await httpClient.GetFromJsonAsync<T>($"{_configuration.GetSection("ERPService:UrlDynamics").Value}/data/{Entity}?cross-company=true{param}");
                //var response = await httpClient.GetAsync($"{_configuration.GetSection("ERPService:UrlDynamics").Value}/data/{Entity}?cross-company=true{param}");
                
                //string strs = await httpClient.GetStringAsync($"/data/{Entity}?cross-company=true{param}");
                //var str = System.Text.Json.JsonSerializer.Deserialize<T>(strs);
                return str;
            }
            catch (Exception ex)
            {
                //this._Validation.Add("Erro na execução do oData: " + ex.Message);
            }
            return Activator.CreateInstance<T>();

        }

        public async Task<T> Post<T>(string Entity, IModel model)
        {
            try
            {
                var body = JsonSerializer.Serialize(model);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"data/{Entity}", content);
                if (response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    var result = JsonSerializer.Deserialize<T>(str);

                    return result;
                }
            }
            catch (Exception ex)
            {
                //this._Validation.Add("Erro na execução do post no oData: " + ex.Message);
            }

            return Activator.CreateInstance<T>();
        }

        public async Task<HttpClient> configureClient()
        {
            HttpClient httpClient = new HttpClient();

            AuthenticationResult result = await ObterCabecalhoAutenticacao();
            httpClient.Timeout = TimeSpan.FromMinutes(20);  // 2 minutes  
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private async Task<AuthenticationResult> ObterCabecalhoAutenticacao(bool useWebAppAuthentication = false)
        {

            try
            {
                string aadClientAppId  = _configuration.GetSection("ERPService:ClientIdDynamics").Value;
                string aadClientSecret = _configuration.GetSection("ERPService:ClientSecret").Value;
                string aadResource     = _configuration.GetSection("ERPService:UrlDynamics").Value;
                string aadTenant       = _configuration.GetSection("ERPService:ActiveDirectoryTenant").Value;

                AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, false);
                AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(aadResource, new ClientCredential(aadClientAppId, aadClientSecret));
                return authenticationResult;
            }
            catch (Exception ex)
            {
                //this._Validation.Add(ex.Message);
                return null;
            }
        }
    }
}
