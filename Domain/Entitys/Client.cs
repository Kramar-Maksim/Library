using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Client
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; } 

        public Client()
        {
            OrderedBooks = new List<Book>();
            DesiredBooks = new List<Order>();
        }

        /// <summary>
        /// Books wich client have
        /// </summary>
        public ICollection<Book> OrderedBooks { get; set; }         //books that cllient allready have
        public ICollection<Order> DesiredBooks { get; set; }        //book thet librerian shud give to client
         
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
