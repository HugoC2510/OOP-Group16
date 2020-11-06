using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class Teacher : Person
    {
        public List<WorkGroup> groups;
        public string password;
        
        public Teacher(string _ID, string _name, string _surname, int _age, char _sex, string _email, string _phoneNumber, string _password)
            :base( _ID, _name,_surname, _age,  _sex, _email,  _phoneNumber, _phoneNumber)
        {
            this.status = "Teacher";
        }
        public override void Tostring() //we show on the console a description of the object
        {
            Console.WriteLine("Name : " + name
                + "; Surname : " + surname
                + "; Age : " + age
                + "; Sex : " + sex
                + "; Email : " + email
                + "; Phone Number : " +phoneNumber
                + "; ID : "+ID
                + "; Status : " +status);
        }
        public override bool ConnectionCheck()
        {
            bool res = false;
            Console.WriteLine("What is your password ? "); //we ask the teacher to write his password
            string password = Console.ReadLine();
            int tries = 2;
            while (password != this.password && tries > 0)
            {
                Console.WriteLine("The password is incorrect, try again (you still have " + tries + " tries)");
                password = Console.ReadLine();
                tries--;
            }
            if(password==this.password)
            {
                res = true;
            }
            return res;
        }
        
        

        
    }
}
