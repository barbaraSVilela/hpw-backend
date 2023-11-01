using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace HPW.Bindings{
public class AuthTokenBinding : IBinding
    {
        private const string HttpRequestBindingName = "$request";
        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) => null;

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            var request = context.BindingData[HttpRequestBindingName] as HttpRequest;
            var valueProvider = new AuthTokenValueProvider(request);
            return Task.FromResult<IValueProvider>(valueProvider);
        }

        public ParameterDescriptor ToParameterDescriptor() => null;
    }
}
