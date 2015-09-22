using System;
using System.Collections.Generic;
using System.Text;

namespace ISD_Final_Project
{
    class Product:Property
    {

        public Product()
        {
        }


       public Product(String name, double quantity, double price): base(name, quantity, price)
        {
            
        }

        public Product(String id, String name, double quantity, double price)
            : base(id, name, quantity, price)
        {

        }



        public string toString()
        {
            return name + ", " + quantity + ", " + price;
        }










    }

}
