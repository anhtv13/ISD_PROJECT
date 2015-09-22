using System;
using System.Collections.Generic;
using System.Text;

namespace ISD_Final_Project
{
    class Ingredient: Property
    {
        private String status;
        public String getStatus(){
            return status;
        }

        public Ingredient()
        {
        }


        public Ingredient( String name, double quantity, double price, String status): base(name, quantity, price)
        {
            this.status = status;
        }

        public Ingredient(String id, String name, double quantity, double price, String status)
            : base(id, name, quantity,price)
        {
            this.status = status;
        }



        public String toString()
        {
            return name + ", " + quantity + ", " + price+ ", " + status;
        }


    }
}
