using ComputerShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain.Helper
{
    public static class ParserHelper
    {
        public static List<OrderItem> ParseStatistics(List<SQLRaw> sQLRaws)
        {
            var result = new List<OrderItem>();

            foreach(var raw in sQLRaws)
            {
                var orderItem = new OrderItem();
                orderItem.Id = Guid.Parse(raw.Fields.FirstOrDefault(x => x.Key == "Id").Value.ToString());
                orderItem.KitId = Guid.Parse(raw.Fields.FirstOrDefault(x => x.Key == "KitId").Value.ToString());
                orderItem.OrderId = Guid.Parse(raw.Fields.FirstOrDefault(x => x.Key == "OrderId").Value.ToString());
                orderItem.CreatedDate = DateTime.Parse(raw.Fields.FirstOrDefault(x => x.Key == "CreatedDate").Value.ToString());

                result.Add(orderItem);
            }
            return result;

        }
    }
}
