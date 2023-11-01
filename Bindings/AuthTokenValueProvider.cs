using System;
using System.Threading.Tasks;
using HPW.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace HPW.Bindings
{
    public class AuthTokenValueProvider : IValueProvider
    {
        private const string AuthHeaderName = "Authorization";
        private const string BearerPrefix = "Bearer ";
        private const string ValidDomain = "dev-t3p4qo2dv6qmgfte.us.auth0.com";

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
                return await GetUserFromGoogleJwtTokenAsync(idToken);
            }

            return null;
        }

        public string ToInvokeString()
        {
            throw new NotImplementedException();
        }

        private async Task<User> GetUserFromGoogleJwtTokenAsync(string token)
        {
            throw new NotImplementedException();
            try
            {
                // var payload = await Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(token, new ValidationSettings{
                //     ExpirationTimeClockTolerance = new TimeSpan(24, 0, 0)
                // });
                // if (payload.HostedDomain != ValidDomain)
                // {
                //     return null;
                // }

                // return new User
                // {
                //     GoogleId = payload.Subject,
                //     Email = payload.Email,
                //     Name = payload.Name
                // };
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}