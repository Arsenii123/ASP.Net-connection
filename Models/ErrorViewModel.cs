namespace Homework2.Models
{
    /// <summary>
    /// Модель для відображення інформації про помилку.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Ідентифікатор запиту, під час якого виникла помилка.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Вказує, чи потрібно показувати ідентифікатор запиту.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
