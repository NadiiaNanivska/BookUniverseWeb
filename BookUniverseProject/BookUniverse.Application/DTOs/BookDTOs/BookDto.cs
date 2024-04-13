namespace BookUniverse.Application.DTOs.BookDTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }

        public int NumberOfPages { get; set; }

        public double Rating { get; set; } = 0.0;

        public int CategoryId { get; set; }
    }
}
