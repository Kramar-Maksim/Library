using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        public string Ganer { get; set; }

        public string Author { get; set; }
        
    }
}
