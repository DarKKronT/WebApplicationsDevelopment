namespace Library.Models
{
    public sealed class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int Year { get; set; }
    }
}
