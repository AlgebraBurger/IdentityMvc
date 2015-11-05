using CrmCoreLite.Areas.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Orders.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }

        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }


    }
}
