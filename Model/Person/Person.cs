using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISD_Project.Model.Person
{
    class Person
    {
        private String id;
        private String name;
       
       
        private String address;
        private String email;
        private String phone;
        private String birthday;


        public String getId()
        {
            return id;
        }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    
    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

   

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public Person()
    {
    }

    public Person(String name, String address, String email, String phone, String birthday) {
            this.name = name; 
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.birthday = birthday;
            }


    public Person(String id, String name, String address, String email, String phone, String birthday)
    {
        this.id = id;
        this.name = name;
        this.address = address;
        this.email = email;
        this.phone = phone;
        this.birthday = birthday;
    }

    public bool validateName(String name)
    {
        if (name.Length < 2 || name == null)
        {
            return false;
        }
        return true;
    }

   

   



    public bool validateBirthday(String birthday)
    {
        //DateTime dateTime = DateTime.ParseExact(birthday, "yyyy-mm-dd", null);
        DateTime dt;
        dt = DateTime.Parse(birthday);
        if (DateTime.TryParse(birthday, out dt))
        {
            return true;
        }
        return false;
    }



    public bool validateAll(String name, String address, String email, String phone, String birthday)
    {
        if (validateName(name) && validateBirthday(birthday) )
        {
            return true;
        }
        return false;
    }

    public bool repOK()
    {
        if(validateAll(name, address, email, phone, birthday)){
            return true;
        }
        return false;
    }


    }
}
