using System;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }

        public BookDTO OrderedBook { get; set; }

        public ClientDTO Clientdto { get; set; }

        public DateTime OrderDate { get; set; }

        public bool IsGiven { get; set; }
    }
}
