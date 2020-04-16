using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class Order
    {
        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }

        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { 
            get
            {
                return OrderProducts.Select(orderProduct => orderProduct.Quantity * orderProduct.Price).Sum();  
            }
        }

        public IList<OrderProduct> OrderProducts { get; private set; }
    }
}