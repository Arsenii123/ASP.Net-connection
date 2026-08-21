namespace Homework2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Director { get; set; }

        public string? Genre { get; set; }

        public FileModel? Poster { get; set; }

        public string? Description { get; set; }
                   
        public int Age { get; set; }
       
    }
}
