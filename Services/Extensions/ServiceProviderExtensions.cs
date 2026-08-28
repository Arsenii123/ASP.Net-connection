using Homework2.Services.Interfaces;

namespace Homework2.Services.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMyService,FirstService>();
            services.AddTransient<IMyService2, SecondService>();
        }

    }
}
