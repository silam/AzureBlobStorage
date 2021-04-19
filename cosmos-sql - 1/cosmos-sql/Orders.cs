using System;
using System.Collections.Generic;
using System.Text;

namespace cosmos_sql
{
    class Orders
    {
        public int orderid { get; set; }
        public int quantity { get; set; }

        public Orders(int p_id,int p_quantity)
        {
            orderid = p_id;
            quantity = p_quantity;
        }
    }
}
