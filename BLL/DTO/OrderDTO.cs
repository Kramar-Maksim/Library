using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }

        [Required]
        public int OrderBook_Id { get; set; } 
        [Required]
        public string ClientOrder_Id { get; set; }

        //[Required]
        //public BookDTO OrderedBook { get; set; }

       // [Required]
       // public ClientDTO Clientdto { get; set; }

       // [Required]
        public DateTime OrderDate { get; set; }

        public bool IsGiven { get; set; }
    }
}
