using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
namespace Homework2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Обов'язкове має бути заповненим")]
        [Display(Name = "Назва")]
        [MaxLength(200,ErrorMessage ="Максимум 200 символів")]
        [MinLength(1,ErrorMessage ="Мінімум як 1 символ")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Обов'язкове має бути заповненим")]
        [Display(Name = "Директор")]
        [MaxLength(200, ErrorMessage = "Максимум 200 символів")]
        [MinLength(1, ErrorMessage = "Мінімум як 1 символ")]
        public string? Director { get; set; }
        [Required(ErrorMessage = "Обов'язкове має бути заповненим")]
        [Display(Name = "Жанр")]
        [MaxLength(200, ErrorMessage = "Максимум 200 символів")]
        [MinLength(1, ErrorMessage = "Мінімум як 1 символ")]

        public string? Genre { get; set; }

        [Display(Name = "Постер")]
        public FileModel? Poster { get; set; }
        [Required(ErrorMessage = "Обов'язкове має бути заповненим")]
        [Display(Name = "Опис")]
        [MaxLength(500, ErrorMessage = "Максимум 500 символів")]
        [MinLength(1, ErrorMessage = "Мінімум як 1 символ")]

        public string? Description { get; set; }
        [Required(ErrorMessage = "Обов'язкове має бути заповненим")]
        [Display(Name="Вік")]
        [Range(1,137,ErrorMessage ="Діапазон віку 1 до 137")]
                   
        public int Age { get; set; }
       
    }
}
