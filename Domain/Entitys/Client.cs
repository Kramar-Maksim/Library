using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Client
    { 
        public Client()
        {
            OrderedBooks = new List<Book>();
            DesiredBooks = new List<Order>();
        }

        public int ClientID { get; set; }

        //[Key]
        //[ForeignKey("ApplicationUser")]
        //  public string IdentityId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
        
        public string Name { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

       
         
        public ICollection<Book> OrderedBooks { get; set; }         //books that cllient allready have
        public ICollection<Order> DesiredBooks { get; set; }        //book thet librerian shud give to client
    }
}
