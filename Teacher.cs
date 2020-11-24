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
        //public List<string> courses;
        public List<Course> courses;
        
        public Teacher(string _ID, string _name, string _surname, int _age, string _sex, string _email, string _phoneNumber, string _password)
            :base( _ID, _name,_surname, _age,  _sex, _email,  _phoneNumber, _password)
        {
            this.status = "Professor";
            this.groups = new List<WorkGroup>();
            this.courses = new List<Course>();
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
            Console.WriteLine("you are registered as a teacher in the following courses: ");
            foreach(Course course in courses)
            {
                Console.WriteLine(course.name);
            }
            Console.WriteLine();
            Console.WriteLine("Your workgroups: ");
            foreach(WorkGroup gp in groups)
            {
                Console.WriteLine(gp.name);
            }

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

        public void ModifyMarksInCourses()
        {
            bool coursenotempty = false;
            Console.Write("you are registered as a professor in the folowing courses: ");
            for(int i=0;i<courses.Count-1;i++)
            {
                Console.Write(courses[i].name + ", ");
            }
            try
            {
                Console.Write(courses[courses.Count - 1].name);
                coursenotempty = true;
            }
            catch
            {
                Console.WriteLine("you do not have any courses for now.");
            }
            if (coursenotempty == true)
            {
                Console.WriteLine("type the name of the course where you want to modify marks");
                string answer =Console.ReadLine();
                foreach(Course course in this.courses)
                {
                    if (answer == course.name)
                    {
                        Console.WriteLine("You can only modify marks of your students.");
                        Console.WriteLine("here is the list of the student you are in charge:");
                        foreach(WorkGroup group in course.allGroups)
                        {
                            try
                            {
                                if (group.professor.ID == this.ID)
                                {
                                    Console.WriteLine("students in the workgroup : " + group.name);
                                    foreach (Student student in group.members)
                                    {
                                        Console.WriteLine(student.ID + " " + student.name + " " + student.surname);
                                    }
                                }
                            }
                            catch { }                       
                        }
                        Console.WriteLine();
                        Console.WriteLine("Choose the student you want to modify its marks : type its id");
                        string answer2 = Console.ReadLine();
                        foreach (WorkGroup group in course.allGroups)
                        {
                            try
                            {
                                if (group.professor.ID == this.ID)
                                {
                                    foreach (Student student in group.members)
                                    {
                                        if (student.ID == answer2)
                                        {
                                            course.ModifyMark(student);
                                        }
                                    }
                                }
                            }
                            catch
                            {

                            }                          
                        }
                    }
                }
            }
        }

        public void ModifyAttendanceInCourses()
        {
            bool coursenotempty = false;
            Console.Write("you are registered as a professor in the folowing courses: ");
            for (int i = 0; i < courses.Count - 1; i++)
            {
                Console.Write(courses[i].name + ", ");
            }
            try
            {
                Console.Write(courses[courses.Count - 1].name);
                coursenotempty = true;
            }
            catch
            {
                Console.WriteLine("you do not have any courses for now.");
            }
            if (coursenotempty == true)
            {
                Console.WriteLine("type the name of the course where you want to modify attendance");
                string answer = Console.ReadLine();
                foreach (Course course in this.courses)
                {
                    if (answer == course.name)
                    {
                        Console.WriteLine("You can only modify attendance of your students.");
                        Console.WriteLine("here is the list of the student you are in charge:");
                        foreach (WorkGroup group in course.allGroups)
                        {
                            if (group.professor.ID == this.ID)
                            {
                                Console.WriteLine("students in the workgroup : " + group.name);
                                foreach (Student student in group.members)
                                {
                                    Console.WriteLine(student.ID + " " + student.name + " " + student.surname);
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Choose the student you want to modify its attendance : type its id");
                        string answer2 = Console.ReadLine();
                        foreach (WorkGroup group in course.allGroups)
                        {
                            if (group.professor.ID == this.ID)
                            {
                                foreach (Student student in group.members)
                                {
                                    if (student.ID == answer2)
                                    {
                                        course.ModifyAttendance(student);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



    }
}
