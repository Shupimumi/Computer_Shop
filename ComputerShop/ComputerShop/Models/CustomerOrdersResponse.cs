using ComputerShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerShop.Models
{
    public class CustomerOrdersResponse
    {
        public Order ActiveOrder { get; set; }
        public decimal ActiveOrderTotalPrice { get; set; }
        public List<Order> History { get; set; }
    }


}
