using HPW.Bindings.Attributes;
using Microsoft.Azure.WebJobs.Host.Config;

namespace HPW.Bindings
{
    public class AuthTokenExtensionProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var provider = new AuthTokenBindingProvider();
            context.AddBindingRule<AuthTokenAttribute>().Bind(provider);
        }
    }
}