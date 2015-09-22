using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISD_Project.Model.Financial
{
    class Income
    {
        private String customerid;
        private double total;
        private String datetime;

        public String getCustomerid()
        {
            return customerid;
        }
        public double getTotal()
        {
            return total;
        }
        public String getDatetime()
        {
            return datetime;
        }





        public Income(String customerid, double total,String datetime)
        {
            this.customerid = customerid;
            this.total = total;
            this.datetime = datetime;
 
        }


        public String toString()
        {
            return "Income {Customer: "+customerid+", $"+total+"}      "+datetime;
        }



    }
}
