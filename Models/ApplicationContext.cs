using Microsoft.EntityFrameworkCore;

namespace Homework2.Models
{
    /// <summary>
    /// Контекст Entity Framework для роботи з файлами.
    /// (Наразі не використовується в Program.cs)
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Набір сутностей, що відповідає таблиці файлів.
        /// </summary>
        public DbSet<FileModel> Files { get; set; }

        /// <summary>
        /// Ініціалізує новий екземпляр <see cref="ApplicationContext"/>.
        /// Автоматично створює базу даних, якщо вона ще не існує.
        /// </summary>
        /// <param name="options">Параметри підключення до бази даних (через DI).</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated(); // автоматично створює базу даних, якщо її ще немає
        }
    }
}
