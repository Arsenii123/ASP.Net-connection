using Microsoft.EntityFrameworkCore;
namespace Homework2
{
    using global::Homework2.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Homework2
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

                // Оба контекста
                builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connection));
                // ApplicationContext больше не регистрируем

                builder.Services.AddControllersWithViews();

                var app = builder.Build();

                app.UseStaticFiles();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movie}/{action=Index}/{id?}");

                app.Run();
            }
        }
    }
}
