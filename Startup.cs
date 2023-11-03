
using HPW;
using HPW.Bindings;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HPW.Database;
using HPW.Services;
using Newtonsoft.Json;

[assembly: WebJobsStartup(typeof(Startup))]

namespace HPW
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<AuthTokenExtensionProvider>();
            ConfigureServices(builder.Services);

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services
               .AddSingleton<IDatabaseConfiguration, CosmosConfiguration>()
               .AddSingleton<DatabaseBuilder>()
               .AddSingleton(p => p.GetRequiredService<DatabaseBuilder>().Build());


            services.AddSingleton<IUserService, UserService>();

        }
    }
}