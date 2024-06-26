﻿namespace BookUniverse.Application.DTOs.BookDTOs
{
    public class AddBookDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int CategoryId { get; set; }

        public int NumberOfPages { get; set; }

        public string Path { get; set; }
    }
}
