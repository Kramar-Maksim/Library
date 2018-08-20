using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        [Required]
        public string Ganer { get; set; }

        [Required]
        public string Author { get; set; }

        public ClientDTO ClientId_WhoTakeTheBook { get; set; }
    }
}
