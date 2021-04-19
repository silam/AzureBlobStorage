using System;
using System.Collections.Generic;
using System.Text;

namespace cosmosdb___sp
{
    class Customer
    {
        public string id { get; set; }
        public int customerid { get; set; }
        public string customername { get; set; }
        public string city { get; set; }

        public Customer(int p_id,string p_name,string p_city)
        {
            customerid = p_id;
            customername = p_name;
            city = p_city;
            id=Guid.NewGuid().ToString();

        }
    }
}
