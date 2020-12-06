using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class Student : Person
    {
        //made by: 
        //23168 Hugo Camps
        //23175 Albert De Watrigant
        //23196 Aurelien Delicourt
        //23172 Jean-Marc Hanna
        //22842 Julien Msika
        //22830 Lorenzo Mendes

        public List<WorkGroup> group;
        public List<string> courseList;
        public List<List<string>> marks; //each course will get its ist of marks
        public List<List<string>> attendance; //each course will get its ist of marks
        public string feeStatus;
        public Student(string _name, string _surname, int _age, string _sex, string _email, string _phoneNumber, string _ID, string password)
            : base(_ID, _name, _surname, _age, _sex, _email, _phoneNumber, password)

        {
            this.status = "Student";
            this.feeStatus = "";
            this.group = new List<WorkGroup>();
            this.courseList = new List<string>();
            this.marks = new List<List<string>>();
            this.attendance = new List<List<string>>();
        }

       
        public override void Tostring() //we show on the console a description of the object
        {
            Console.WriteLine("Name : " + name
                + "\n Surname : " + surname
                + "\n Age : " + age
                + "\n Sex : " + sex
                + "\n Email : " + email
                + "\n Phone Number : " + phoneNumber
                + "\n ID : " + ID
                + "\n Status : " + status);
            Console.WriteLine();
            Console.WriteLine("you are registered as a student in the following courses: ");
            foreach (string course in courseList)
            {
                Console.WriteLine(course);
            }
            Console.WriteLine();
            Console.WriteLine("You are part of the following workgroup");
            foreach (WorkGroup gp in group)
            {
                Console.WriteLine(gp.name);
            }
        }

        public void DisplayFeeStatues()
        {
            Console.WriteLine("Status of your finance:");
            Console.WriteLine(feeStatus);
            Console.WriteLine();
        }
        public void DisplayAllMarks()
        {
            double total = 0;

            int count = 0;
            foreach (List<string> coursemark in marks)
            {
                foreach (string mark in coursemark)
                {
                    Console.Write(mark + " ");
                }
                Console.WriteLine();
                if (coursemark.Count() > 1) ;
                {
                    for (int i = 1; i < coursemark.Count(); i++)
                    {
                        double newMark = Convert.ToDouble(coursemark[i]);
                        total += newMark;
                        count++;
                    }
                }
                double total2 = 0;

                int count2 = 0;
                if (coursemark.Count() > 1) ;
                {
                    for (int i = 1; i < coursemark.Count(); i++)
                    {
                        double newMark = Convert.ToDouble(coursemark[i]);
                        total2 += newMark;
                        count2++;
                    }

                }
                Console.WriteLine("the mean of the student in " + coursemark[0] + total2 / count2);
                Console.WriteLine();
            }
            Console.WriteLine("the general mean of the student is : " + total / count);
            Console.WriteLine();
        }

        public void DisplayAllAttendanceReport()
        {
            foreach (List<string> courseReport in attendance)
            {
                foreach (string report in courseReport)
                {
                    Console.Write(report + " ");
                }
                Console.WriteLine();
            }
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
        public int FindStudentInList(List<Student> list) //return the indexe of the researched student in the list
        {
            Console.WriteLine("To find the student, type Firstname or ID ");
            string firstName = Console.ReadLine().ToUpper();
            bool numeric = true;
            try
            {
                int.Parse(firstName);
            }
            catch
            {
                numeric = false;
            }
            int line;
            bool found = false;
            if (numeric == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ID == firstName)
                    {
                        found = true;
                        return i;
                    }
                }

            }
            else
            {
                Console.WriteLine("Enter Surname ");
                string surName = Console.ReadLine().ToUpper();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].name.ToUpper() == firstName && list[i].surname.ToUpper() == surName)
                    {
                        found = true;
                        return i;
                    }
                }
                              
            }
            return -1; //return -1 if nothing is found.
        }
        
    }   
}
