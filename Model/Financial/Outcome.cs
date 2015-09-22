using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISD_Project.Model.Financial
{
    class Outcome
    {
        private int id;
        private double salary;
        private double ingredientfee;
        private double electricity;
        private double water;
        private double internet;
        private double othefee;

        public double getSalary()
        {
            return salary;
        }
        public double getIngredientFee()
        {
            return ingredientfee;
        }
        public double getElectricity()
        {
            return electricity;
        }
        public double getWater()
        {
            return water;
        }
        public double getInternet()
        {
            return internet;
        }
        public double getOtherFee(){
            return othefee;
        }

        public Outcome()
        {
        }
        public Outcome(int id, double salary, double ingredientfee, double electricity, double water, double internet, double othefee)
        {
            this.id = id;
            this.salary = salary;
            this.ingredientfee = ingredientfee;
            this.electricity = electricity;
            this.water = water;
            this.internet = internet;
            this.othefee = othefee;
        }
        public Outcome(double salary, double ingredientfee, double electricity, double water, double internet, double othefee)
        {
            this.salary = salary;
            this.ingredientfee = ingredientfee;
            this.electricity = electricity;
            this.water = water;
            this.internet = internet;
            this.othefee = othefee;
        }
        public Outcome(double electricity, double water, double internet, double othefee)
        {        
            this.electricity = electricity;
            this.water = water;
            this.internet = internet;
            this.othefee = othefee;
        }

        public bool validateNumber(double a)
        {
            if (a > 0)
            {
                return true;
            }
            return false;
        }

        public bool validateAll(double electricity, double water, double internet, double othefee)
        {
            if (validateNumber(electricity) && validateNumber(water) && validateNumber(internet) && validateNumber(othefee))
            {
                return true;
            }
            return false;
        }

        public bool repOK()
        {
            if (validateAll(electricity, water, internet, othefee) == true)
            {
                return true;
            }
            return false;
        }



    }
}
