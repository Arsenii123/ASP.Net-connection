using Microsoft.EntityFrameworkCore;

namespace Homework2.Models
{

       
        public class MovieContext : DbContext
        {
            public DbSet<Movie> Movies { get; set; }
            public DbSet<FileModel> Files { get; set; }   // ← добавили

            public MovieContext(DbContextOptions<MovieContext> options) : base(options)
            {
                if (Database.EnsureCreated())
                {
                Movies?.Add(new Movie
                {
                    Name = "Interstellar",
                    Director = "Christopher Nolan",
                    Genre = "Sci-Fi",
                    Poster = new FileModel
                    {

                        Name = "intersteller.webp",
                        Path = "img/intersteller.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "A team of explorers travels through a wormhole in space to ensure humanity's survival.",
                    Age = 2026 - 2014
                });

                Movies?.Add(new Movie
                {
                    Name = "Avatar",
                    Director = "James Cameron",
                    Genre = "Sci-Fi",
                    Poster = new FileModel
                    {

                        Name = "avatar.webp",
                        Path = "img/avatar.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "On Pandora, humans clash with the Na'vi while one soldier finds a new purpose.",
                    Age = 2026 - 2009
                });

                Movies?.Add(new Movie
                {
                    Name = "Spider-Man",
                    Director = "Sam Raimi",
                    Genre = "Superhero",
                    Poster = new FileModel
                    {

                        Name = "spider-man.webp",
                        Path = "img/spider-man.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "Peter Parker gains spider-like powers and must balance heroism with personal life.",
                    Age = 2026 - 2002
                });

                Movies?.Add(new Movie
                {
                    Name = "Venom",
                    Director = "Ruben Fleischer",
                    Genre = "Superhero",
                    Poster = new FileModel
                    {

                        Name = "venom.webp",
                        Path = "img/venom.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "Journalist Eddie Brock bonds with an alien symbiote, becoming the anti-hero Venom.",
                    Age = 2026 - 2018
                });

                Movies?.Add(new Movie
                {
                    Name = "Oppenheimer",
                    Director = "Christopher Nolan",
                    Genre = "Biography",
                    Poster = new FileModel
                    {

                        Name = "oppenheimer.webp",
                        Path = "img/oppenheimer.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "The story of J. Robert Oppenheimer and the creation of the atomic bomb.",
                    Age = 2026 - 2023
                });

                Movies?.Add(new Movie
                {
                    Name = "The Hobbit",
                    Director = "Peter Jackson",
                    Genre = "Fantasy",
                    Poster = new FileModel
                    {

                        Name = "the hobbit.webp",
                        Path = "img/the hobbit.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "Bilbo Baggins embarks on a journey with dwarves to reclaim their homeland.",
                    Age = 2026 - 2012
                });

                Movies?.Add(new Movie
                {
                    Name = "The Avengers",
                    Director = "Joss Whedon",
                    Genre = "Superhero",
                    Poster = new FileModel
                    {

                        Name = "the avengers.webp",
                        Path = "img/the avengers.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "Earth's mightiest heroes unite to stop Loki and his alien army.",
                    Age = 2026 - 2012
                });

                Movies?.Add(new Movie
                {
                    Name = "Harry Potter and the Sorcerer's Stone",
                    Director = "Chris Columbus",
                    Genre = "Fantasy",
                    Poster = new FileModel
                    {

                        Name = "harry potter.webp",
                        Path = "img/harry potter.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "A young boy discovers he is a wizard and attends Hogwarts School of Witchcraft.",
                    Age = 2026 - 2001
                });

                Movies?.Add(new Movie
                {
                    Name = "Beautiful Boy",
                    Director = "Felix Van Groeningen",
                    Genre = "Drama",
                    Poster = new FileModel
                    {

                        Name = "beatiful boy.webp",
                        Path = "img/beatiful boy.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "A father struggles to help his son through addiction and recovery.",
                    Age = 2026 - 2018
                });

                Movies?.Add(new Movie
                {
                    Name = "Pirates of the Caribbean: The Curse of the Black Pearl",
                    Director = "Gore Verbinski",
                    Genre = "Adventure",
                    Poster = new FileModel
                    {

                        Name = "pirates of the Caribbean.webp",
                        Path = "img/pirates of the Caribbean.webp",
                        UploadDate = DateTime.Now
                    },
                    Description = "Captain Jack Sparrow teams up to rescue a kidnapped maiden from cursed pirates.",
                    Age = 2026 - 2003
                });



                SaveChanges();
                SaveChanges();
                }
            }
        }
    }



