using Homework2.Services.Interfaces;

namespace Homework2.Services.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICreate,CreateService>();
            services.AddTransient<IEdit, EditService>();
            services.AddTransient<IDelete, DeleteService>();
        }

    }
}
