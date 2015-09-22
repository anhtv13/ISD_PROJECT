using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISD_Project.Model.Person
{
    class Supplier:Person
    {
       
        private String ingredientid;

        public String getIngredientid()
        {
            return ingredientid;
        }

        public Supplier(String name, String address, String email, String phone, String birthday, String ingredientid)
            : base(name,  address, email, phone, birthday)
        {
            this.ingredientid = ingredientid;
        }

        public Supplier(String id, String name, String address, String email, String phone, String birthday, String ingredientid)
            : base(id, name, address, email, phone, birthday)
        {
            this.ingredientid = ingredientid;
        }

        public Supplier()
        {
        }


        public String toString()
        {
            return "name=" + getName() + ", address=" + getAddress() + ", email=" + getEmail() +
                ", phone=" + getPhone() + ", birthday=" + getBirthday() + ", ingredient_id=" + getIngredientid();
        }


        public String toStringInfo()
        {
            return getName() + ", " + getAddress() + ", " + getEmail() +
                ", " + getPhone() + ", " + getBirthday() + ", " + getIngredientid();
        }

        public bool validateIngredientid(String ingredientid)
        {
            if (ingredientid.Equals("") || ingredientid ==null)
            {
                return false;
            }
            return true;
        }


        public bool validateAll(String name, String address, String email, String phone, String birthday, String ingrdientid)
        {
            if (validateName(name) && validateBirthday(birthday) && validateIngredientid(ingrdientid))
            {
                return true;
            }
            return false;
        }



        public bool repOK()
        {
            if (validateAll(getName(), getAddress(), getEmail(), getPhone(), getBirthday(), getIngredientid()))
            {
                return true;
            }
            return false;
        }








    }
}
