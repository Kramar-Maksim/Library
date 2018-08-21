using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }

       
        //public Book OrderedBook { get; set; }
        [Required]
        public int OrderBook_Id { get; set; }

       
        //public Client ClientOrder { get; set; }
        [Required]
        public string ClientOrder_Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// boolean, shows if librarian serve the order or not
        /// </summary>
        public bool IsGiven { get; set; }
    }
}
