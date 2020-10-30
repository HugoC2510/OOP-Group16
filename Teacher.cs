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
        public string password;
        
        public Teacher(string _ID, string _name, string _surname, int _age, char _sex, string _email, string _phoneNumber, string _password)
            :base( _ID, _name,_surname, _age,  _sex, _email,  _phoneNumber)
        {
            this.status = "Teacher";
            this.password = _password;
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
        public void ManageAttendance()
        {
            bool valid = ConnectionCheck();
            if(valid==true)
            {
                Console.WriteLine("What is the student's last name : ");
                string stud = Console.ReadLine().ToUpper();
                foreach(WorkGroup w in groups)
                {
                    foreach(Student s in w.members)
                    {
                        if(s.name.ToUpper()==stud)
                        {
                            s.absence += 1;
                        }
                    }
                }
            }
        }

        
    }
}
