using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class Student : Person
    {
        public YearGrade year;
        public WorkGroup group;
        public string program;
        public List<int> marks;
        public int fees;
        public Student(string _name, string _surname,int _age,char _sex,string _email, string _phoneNumber, string _ID,WorkGroup _group, string _program, List<int> _marks, int _fees, YearGrade _year)
            : base(_ID, _name, _surname, _age, _sex, _email, _phoneNumber)
        {
            this.status = "Student";
            this.year = _year;
            this.group = _group;
            this.fees = _fees;
            this.marks = _marks;
            this.program = _program;
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
                + "; Workgroup : "+ group.name
                + "; Year grade : "+year.year
                + "; Program : "+program
                + "; Status : " + status);
        }
    }
}
