using System;

namespace Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }

        public Book OrderedBook { get; set; }

        public Client ClientOrder { get; set; }

        public DateTime OrderDate { get; set; }

        public bool IsGiven { get; set; }
    }
}
