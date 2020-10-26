using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class Teacher : Person
    {
        public List<WorkGroup> groups;
        
        public Teacher(string _ID, string _name, string _surname, int _age, char _sex, string _email, string _phoneNumber, List<WorkGroup> _groups)
            :base( _ID, _name,_surname, _age,  _sex, _email,  _phoneNumber)
        {
            this.status = "Teacher";
            this.groups = _groups;
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
    }
}
