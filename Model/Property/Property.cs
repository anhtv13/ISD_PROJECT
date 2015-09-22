using System;
using System.Collections.Generic;
using System.Text;

namespace ISD_Final_Project
{
    class Property
    {




        public String id;
        public String name;
        public double quantity;
        public double price;


        public String getId()
        {
            return id;
        }
        public String getName()
        {
            return name;
        }
        public double getQuantity()
        {
            return quantity;
        }
        public double getPrice()
        {
            return price;
        }
       


       
        

        
        


        
        public bool validateName(string name)
        {
            if (name == null || name == "") return false;
            return true;
        }
        public bool validateQuantity(double quantity)
        {
            if (quantity<=0) return false;
            return true;
        }
        public bool validatePrice(double price)
        {
            if (price <= 0) return false;
            return true;
        }
        public bool validateAll(String name, double quantity, double price)
        {
            if (validateName(name) && validateQuantity(quantity) && validatePrice(price))
                return true;
            return false;
        }
        public bool repOK()
        {
            if(validateAll(name, quantity, price))
                return true;
            return false;
        }
        public Property() { }



        public Property(String name, double quantity, double price)
        {
            
               
                this.name = name;
                this.quantity = quantity;
                this.price = price;
       
            
        }


        public Property(String id, String name, double quantity, double price)
        {

            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }

        public String toString()
        {
            return name + ", " + quantity + ", " + price;
        }







    }
}
