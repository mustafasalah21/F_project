using Microsoft.Extensions.DependencyInjection;
using ULearn.infrastructure.Implementation;

namespace ULearn.infrastructure.Factory
{
    public class InfrastructureFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IConfigurationSettings, ConfigurationSettings>();
        }
    }
}
