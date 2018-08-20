using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class ClientDTO
    {
        public string ClientID { get; set; }

        public string IdentityId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Adress { get; set; }

        [Required]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public List<BookDTO> OrderedBooks { get; set; }         //books that cllient allready have
        public List<OrderDTO> DesiredBooks { get; set; }        //book thet librerian shud give to client

        public ClientDTO()
        {
            OrderedBooks = new List<BookDTO>();
            DesiredBooks = new List<OrderDTO>();
        }
    }
}
