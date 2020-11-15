using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class Administrator: Person
    {
        public YearGrade year;
        public string password;
        public Administrator(string _ID, string _name, string _surname, int _age, string _sex, string _email, string _phoneNumber,string _password)
            :base(_ID, _name,_surname, _age, _sex, _email, _phoneNumber, _password)
        {
            this.status = "Administrator";
        }

        public override void Tostring() //we show on the console a description of the object
        {
            Console.WriteLine("Name : " + name
                + "; Surname : " + surname
                + "; Age : " + age
                + "; Sex : " + sex
                + "; Email : " + email
                + "; Phone Number : " + phoneNumber
                + "; ID : " + ID
                + "; Status : " + status);
        }
        public override bool ConnectionCheck()
        {
            bool res = false;
            Console.WriteLine("What is your password ? "); //we ask the administrator to write his password
            string password = Console.ReadLine();
            int tries = 2;
            while (password != this.password && tries > 0)
            {
                Console.WriteLine("The password is incorrect, try again (you still have " + tries + " tries)");
                password = Console.ReadLine();
                tries--;
            }
            if (password == this.password)
            {
                res = true;
            }
            return res;
        }
        
        
        public Student FindStudent(List<Student> elements)
        {
            
            Console.WriteLine("Which student do you want to find (write his first name) ?");
            string first = Console.ReadLine().ToUpper();
            Console.WriteLine("Write his last name : ");
            string last = Console.ReadLine().ToUpper();
            foreach(Student s in elements)
            {
                if(s.name.ToUpper()==first && s.surname.ToUpper()==last)
                {
                    return s;
                }
                

            }
            return null;
        }
    }
}
