using System.Collections.Generic;

namespace BLL.DTO
{
    public class ClientDTO
    {
        public ICollection<BookDTO> OrderedBooks { get; set; }         //books that cllient allready have
        public ICollection<OrderDTO> DesiredBooks { get; set; }        //book thet librerian shud give to client

        //public ClientDTO()
        //{
        //    OrderedBooks = new List<BookDTO>();
        //    DesiredBooks = new List<OrderDTO>();
        //}

        public int ClientID { get; set; }


        public string IdentityId { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
