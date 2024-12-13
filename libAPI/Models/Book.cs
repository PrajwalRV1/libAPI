namespace libAPI.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }
        public int YearPublished { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
