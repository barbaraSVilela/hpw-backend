using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace HPW.Bindings{
    public class AuthTokenBindingProvider : IBindingProvider
    {
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            var binding = new AuthTokenBinding();
            return Task.FromResult<IBinding>(binding);
        }
    }
}