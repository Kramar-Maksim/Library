using System;

namespace BLL.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Year { get; set; }

        public string Ganer { get; set; }

        public string Author { get; set; }

        //public int AuthorId { get; set; }
        //public AuthorDTO Author { get; set; }
    }
}
