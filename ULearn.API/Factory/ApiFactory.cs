using Microsoft.Extensions.DependencyInjection;
using ULearn.Core.Factory;

namespace ULearn.API.Factory
{
    public class ApiFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            DataManagerFactory.RegisterDependencies(services);
        }
    }
}
