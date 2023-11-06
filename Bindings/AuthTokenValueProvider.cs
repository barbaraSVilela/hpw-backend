using System;
using System.Net.Http;
using System.Threading.Tasks;
using HPW.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Newtonsoft.Json;

namespace HPW.Bindings
{
    public class AuthTokenValueProvider : IValueProvider
    {
        private const string AuthHeaderName = "Authorization";
        private const string BearerPrefix = "Bearer ";
        //TODO: Extract this to the settings.json file
        private const string UserInfoEndpoint = "https://dev-t3p4qo2dv6qmgfte.us.auth0.com/userinfo";

        private readonly HttpRequest _httpRequest;

        public AuthTokenValueProvider(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public Type Type => typeof(User);

        public async Task<object> GetValueAsync()
        {
            if (_httpRequest.Headers.TryGetValue(AuthHeaderName, out var authorization)
                && authorization.ToString().StartsWith(BearerPrefix))
            {
                var idToken = authorization.ToString().Substring(BearerPrefix.Length);
                return await GetUserInfo(idToken);
            }

            return null;
        }

        public string ToInvokeString()
        {
            return String.Empty;
        }


        //TODO: Extract this to a Auth0 specific service, following the Adapter pattern and therefore isolating the third-party library from the rest.
        private async Task<User> GetUserInfo(string token)
        {

            using HttpClient client = new();
            client.DefaultRequestHeaders.TryAddWithoutValidation(AuthHeaderName, $"{BearerPrefix}{token}");
            var response = await client.GetAsync(UserInfoEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(json);
            }
            else
            {
                throw new Exception("Failed to get user info.");
            }

        }
    }
}