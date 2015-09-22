using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISD_Project.Model.Person
{
    class Customer:Person
    {

        private String point;

        public String getPoint()
        {
            return point;
        }

        public Customer()
        {
        }

        public Customer(String name,  String address, String email, String phone, String birthday, String point)
            : base(name,  address, email, phone,birthday)
        {
            this.point = point;
        }


        public Customer(String id, String name, String address, String email, String phone, String birthday, String point)
            : base(id, name, address, email, phone, birthday)
        {
            this.point = point;
        }


        public String toString()
        {
            return "name=" + getName() + ", address=" + getAddress() + ", email=" + getEmail()+
                ", phone=" + getPhone() + ", birthday=" + getBirthday() + ", point=" +getPoint() ;
        }

        public String toStringInfo()
        {
            return getName() +", " +getAddress() + ", " + getEmail() +
                ", " + getPhone() + ", " + getBirthday() + ", " + getPoint();
        }


    }
}
