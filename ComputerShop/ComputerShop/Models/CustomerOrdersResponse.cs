using ComputerShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerShop.Models
{
    public class CustomerOrdersResponse
    {
        public OrderDisplay ActiveOrder { get; set; }
        public List<OrderDisplay> History { get; set; }
    }

    public class OrderDisplay : Order
    {
        public Decimal TotalPrice { get; set; }
    }


}
