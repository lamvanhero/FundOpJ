using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OppJar.Common.Helpers;
using OppJar.Core.Services.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public class TruliooService : ITruliooService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private string _baseUrl => _configuration.GetSection("Trulioo").GetValue<string>("BaseUrl");
        private string _apiKey => _configuration.GetSection("Trulioo").GetValue<string>("ApiKey");

        private const string CONFIGURATION_NAME = "Identity Verification";
        private const string GET_COUNTRY_CODE = "/configuration/v1/countrycodes/{0}";
        private const string GET_FIELDS = "/configuration/v1/fields/{0}/{1}";
        private const string GET_CONSENTS = "/configuration/v1/consents/{0}/{1}";
        private const string VERIFY = "/verifications/v1/verify";
        public TruliooService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _client = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<IEnumerable<string>> GetConsentsAsync(string countryCode)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_baseUrl}{string.Format(GET_CONSENTS, CONFIGURATION_NAME, countryCode)}"),
                Method = HttpMethod.Get
            };

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpRequest.Headers.Add("x-trulioo-api-key", _apiKey);

            var respsonse = await _client.SendAsync(httpRequest);

            respsonse.EnsureSuccessStatusCode();

            var json = await respsonse.Content.ReadAsStringAsync();

            return json.JsonToObj<IEnumerable<string>>();
        }

        public async Task<IEnumerable<string>> GetCountryCodesAsync()
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_baseUrl}{string.Format(GET_COUNTRY_CODE, CONFIGURATION_NAME)}"),
                Method = HttpMethod.Get
            };

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpRequest.Headers.Add("x-trulioo-api-key", _apiKey);

            var respsonse = await _client.SendAsync(httpRequest);

            respsonse.EnsureSuccessStatusCode();

            var json = await respsonse.Content.ReadAsStringAsync();

            return json.JsonToObj<IEnumerable<string>>();
        }

        public async Task<JObject> GetFieldsAsync(string countryCode)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_baseUrl}{string.Format(GET_FIELDS, CONFIGURATION_NAME, countryCode)}"),
                Method = HttpMethod.Get
            };

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpRequest.Headers.Add("x-trulioo-api-key", _apiKey);

            var respsonse = await _client.SendAsync(httpRequest);

            respsonse.EnsureSuccessStatusCode();

            var json = await respsonse.Content.ReadAsStringAsync();

            return JObject.Parse(json);
        }

        public async Task<VerifyResult> VerifyAsync(VerifyRequest request)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_baseUrl}{VERIFY}"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
            };

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpRequest.Headers.Add("x-trulioo-api-key", _apiKey);

            var respsonse = await _client.SendAsync(httpRequest);

            respsonse.EnsureSuccessStatusCode();

            var json = await respsonse.Content.ReadAsStringAsync();

            return json.JsonToObj<VerifyResult>();
        }

        public async Task<string> TestConnectionAsync()
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_baseUrl}/connection/v1/testauthentication"),
                Method = HttpMethod.Get
            };

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpRequest.Headers.Add("x-trulioo-api-key", _apiKey);

            var respsonse = await _client.SendAsync(httpRequest);

            respsonse.EnsureSuccessStatusCode();

            var json = await respsonse.Content.ReadAsStringAsync();

            return json.JsonToObj<string>();
        }
    }
}
