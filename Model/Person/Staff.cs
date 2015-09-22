using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISD_Project.Model.Person
{
    class Staff: Person
    {
       
        private int roleid;


        public Staff(String name,  String address, String email, String phone,String birthday, int roleid)
            : base(name, address, email, phone, birthday)
        {
            this.roleid = roleid;
        }

        public Staff(String id, String name, String address, String email, String phone, String birthday, int roleid)
            : base(id, name, address, email, phone, birthday)
        {
            this.roleid = roleid;
        }

        public Staff()
        {
        }


        public int getRoleId()
        {
            return roleid;
        }


        public String toString()
        {
            return "name=" + getName() + ", address=" + getAddress() + ", email=" + getEmail() +
                ", phone=" + getPhone() + ", birthday=" + getBirthday() + ", role=" + getRoleId();
        }


        public String toStringInfo()
        {
            return getName() + ", " + getAddress() + ", " + getEmail() +
                ", " + getPhone() + ", " + getBirthday() + ", " + getRoleId();
        }

        public bool validateRoleId(int roleid)
        {
            if (roleid<=0)
            {
                return false;
            }
            return true;
        }

        public bool validateAll(String name, String address, String email, String phone, String birthday, int roleid)
        {
            if (validateName(name) && validateBirthday(birthday) && validateRoleId(roleid))
            {
                return true;
            }
            return false;
        }
        public bool repOK()
        {
            if (validateAll(getName(), getAddress(), getEmail(), getPhone(), getBirthday(),  getRoleId()))
            {
                return true;
            }
            return false;
        }












       


    }
}
