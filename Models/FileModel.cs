namespace Homework2.Models
{
    /// <summary>
    /// Модель завантаженого файлу (постера).
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Унікальний ідентифікатор файлу.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Оригінальне ім'я файлу.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Відносний шлях до файлу (наприклад, /img/guid_filename.webp).
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Дата та час завантаження файлу.
        /// </summary>
        public DateTime UploadDate { get; set; }
    }
}
