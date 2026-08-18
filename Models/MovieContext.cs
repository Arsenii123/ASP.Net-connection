using Microsoft.EntityFrameworkCore;

namespace Homework2.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; } // набір сутностей Student, який буде відображено в таблицю Students (ORM)
        public MovieContext(DbContextOptions<MovieContext> options) // конструктор, що приймає параметри підключення
        // options буде отриманий із Program.cs завдяки механізму впровадження залежностей (Dependency Injection)
           : base(options) // передаємо параметри базовому класу DbContext
        {
            if (Database.EnsureCreated()) // якщо база даних ще не створена — створюємо її (одноразово)
            {
                Movies?.Add(new Movie {  Name = "Interstellar", Director = "Christopher Nolan", Genre = "Sci-Fi", Poster = "img/intersteller.webp", Description = "A team of explorers travels through a wormhole in space to ensure humanity's survival.", Age = 2026 - 2014 });

                Movies?.Add(new Movie { Name = "Avatar", Director = "James Cameron", Genre = "Sci-Fi", Poster="img/avatar.webp", Description = "On Pandora, humans clash with the Na'vi while one soldier finds a new purpose.", Age = 2026 - 2009 });

                Movies?.Add(new Movie {  Name = "Spider-Man", Director = "Sam Raimi", Genre = "Superhero", Poster= "img/spider-man.webp", Description = "Peter Parker gains spider-like powers and must balance heroism with personal life.", Age = 2026 - 2002 });

                Movies?.Add(new Movie {  Name = "Venom", Director = "Ruben Fleischer", Genre = "Superhero", Poster = "img/venom.webp", Description = "Journalist Eddie Brock bonds with an alien symbiote, becoming the anti-hero Venom.", Age = 2026 - 2018 });

                Movies?.Add(new Movie {  Name = "Oppenheimer", Director = "Christopher Nolan", Genre = "Biography", Poster = "img/oppenheimer.webp", Description = "The story of J. Robert Oppenheimer and the creation of the atomic bomb.", Age = 2026 - 2023 });

                Movies?.Add(new Movie {  Name = "The Hobbit", Director = "Peter Jackson", Genre = "Fantasy", Poster ="img/the hobbit.webp", Description = "Bilbo Baggins embarks on a journey with dwarves to reclaim their homeland.", Age = 2026 - 2012 });

                Movies?.Add(new Movie {  Name = "The Avengers", Director = "Joss Whedon", Genre = "Superhero", Poster ="img/the avengers.webp", Description = "Earth's mightiest heroes unite to stop Loki and his alien army.", Age = 2026 - 2012 });

                Movies?.Add(new Movie {  Name = "Harry Potter and the Sorcerer's Stone", Director = "Chris Columbus", Genre = "Fantasy", Poster = "img/harry potter.webp", Description = "A young boy discovers he is a wizard and attends Hogwarts School of Witchcraft.", Age = 2026 - 2001 });

                Movies?.Add(new Movie {  Name = "Beautiful Boy", Director = "Felix Van Groeningen", Genre = "Drama", Poster = "img/beatiful boy.webp", Description = "A father struggles to help his son through addiction and recovery.", Age = 2026 - 2018 });

                Movies?.Add(new Movie {  Name = "Pirates of the Caribbean: The Curse of the Black Pearl", Director = "Gore Verbinski", Genre = "Adventure", Poster = "img/pirates of the Caribbean.webp", Description = "Captain Jack Sparrow teams up to rescue a kidnapped maiden from cursed pirates.", Age = 2026 - 2003 });



                SaveChanges(); // зберігаємо початкові дані в базу
            }
        }
    }
}
