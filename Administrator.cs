using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class Administrator: Person
    {
        public List<Course> allcourses;
        public List<Person> allpersons;
        public Database_Login database_Login;
        public Database_CourseList database_CourseList;
        public Database_Fees database_Fees;

        public Administrator(string _ID, string _name, string _surname, int _age, string _sex, string _email, string _phoneNumber,string _password)
            :base(_ID, _name,_surname, _age, _sex, _email, _phoneNumber, _password)
        {
            this.status = "Administrator";
            this.allcourses = new List<Course>();
            this.allpersons = new List<Person>();
            this.database_Login = null;
            this.database_CourseList = null;
            this.database_Fees = null;
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
        }

        public void ModifySomeonesMarks()
        {
            Console.WriteLine("here is the list of all the courses that are currently being done at school" );
            Console.WriteLine();
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            Console.WriteLine("select the course to find the person you want to modify its marks");
            string answer = Console.ReadLine();
            foreach(Course course in allcourses)
            {
                if(course.name == answer)
                {
                    course.ModifyMark();
                }
            }
        }

        public void ModifySomeonesAttendance()
        {
            Console.WriteLine("here is the list of all the courses that are currently being done at school");
            Console.WriteLine();
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            Console.WriteLine("select the course to find the person you want to modify its attendance report");
            string answer = Console.ReadLine();
            foreach (Course course in allcourses)
            {
                if (course.name == answer)
                {
                    course.ModifyAttendance();
                }
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

        public void AddPersonInOrganization()
        {
            database_Login.AddInformation();
        }

        public void ShowProfile()
        {
            Console.WriteLine();
            Person person = Person.FindPersonInOrganization(this.allpersons);
            if(person is Student)
            {
                Student student = person as Student;
                student.Tostring();
            }
            if(person is Teacher)
            {
                Teacher teacher = person as Teacher;
                teacher.Tostring();
            }
            database_Login.ShowInformation();
        }

        public void ModifyProfile()
        {
            database_Login.ModifyFile();
        }

        public void RemovePersonFromOrganization()
        {
            database_Login.RemoveInformation();
        }

        public void CreateNewCourse()
        {
            string answer = "";
            string answer2 = "";
            bool courseCreated = false;
            while(answer!="exit" && answer2 != "exit" && courseCreated ==false)
            {
                Console.WriteLine("type the name of the course you want to create. Type <<exit>> to cancel");
                answer = Console.ReadLine();
                bool courseExist = false;
                foreach(Course cours in this.allcourses)
                {
                    if (cours.name == answer)
                    {
                        Console.WriteLine("A course with the same name already exist. Try another name.");
                        courseExist = true;
                    }
                }
                if (courseExist == false && answer!="exit")
                {
                    Console.WriteLine("type the year grade of the students in it : A1, A2, A3, A4, A5, Phd. Type <<exit>> to cancel");
                    answer2 = Console.ReadLine();
                    if (answer != "exit")
                    {
                        switch (answer2)
                        {
                            case "A1":
                                Course course = new Course(answer, answer2);
                                courseCreated = true;
                                database_CourseList.AddACourse(course);
                                Console.WriteLine("Course created");
                                break;
                            case "A2":
                                Course course2 = new Course(answer, answer2);
                                courseCreated = true;
                                database_CourseList.AddACourse(course2);
                                Console.WriteLine("Course created");
                                break;
                            case "A3":
                                Course course3 = new Course(answer, answer2);
                                courseCreated = true;
                                database_CourseList.AddACourse(course3);
                                Console.WriteLine("Course created");
                                break;
                            case "A4":
                                Course course4 = new Course(answer, answer2);
                                courseCreated = true;
                                database_CourseList.AddACourse(course4);
                                Console.WriteLine("Course created");
                                break;
                            case "A5":
                                Course course5 = new Course(answer, answer2);
                                courseCreated = true;
                                database_CourseList.AddACourse(course5);
                                Console.WriteLine("Course created");
                                break;
                            case "Phd":
                                Course course6 = new Course(answer, answer2);
                                courseCreated = true;
                                database_CourseList.AddACourse(course6);
                                Console.WriteLine("Course created");
                                break;
                            case "exit":
                                break;
                            default:
                                Console.WriteLine("no valid syntax");
                                break;
                        }
                    }
                }                          
            }           
        }

        public void AddPersonToACourse()
        {
            string answer = "";
            bool add = false;
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            while (answer != "exit" && add==false)
            {
                Console.WriteLine("Type the name of the course you want to add a student. Type <<exit>> to leave.");
                answer = Console.ReadLine();
                foreach(Course course in allcourses)
                {
                    if(course.name == answer)
                    {
                        course.AddStudentIntoWorkGroupOfaCourse();
                        add = true;
                    }
                }
                if(add!= true && answer!="exit")
                {
                    Console.WriteLine("This course doesn't exist");
                }                
            }
        }

        public void AddWorkGroupToACourse()
        {
            string answer = "";
            bool add = false;
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            while (answer != "exit" && add == false)
            {
                Console.WriteLine("Type the name of the course you want to add a workGroup. Type <<exit>> to leave.");
                answer = Console.ReadLine();
                foreach (Course course in allcourses)
                {
                    if (course.name == answer)
                    {
                        course.AddWrkGroupToCourse();
                        add = true;
                    }
                }
                if (add != true && answer != "exit")
                {
                    Console.WriteLine("This course doesn't exist");
                }
            }
        }

        public void ModifyTeacherInWorkGroup()
        {
            string answer = "";
            bool add = false;
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            while (answer != "exit" && add == false)
            {
                Console.WriteLine("Type the name of the course you want to modify the teacher. Type <<exit>> to leave.");
                answer = Console.ReadLine();
                foreach (Course course in allcourses)
                {
                    if (course.name == answer)
                    {
                        course.ModifyTeacherIntoWorkGroupOfaCourse();
                        add = true;
                    }
                }
                if (add != true && answer != "exit")
                {
                    Console.WriteLine("This course doesn't exist");
                }
            }
        }

        public void RemoveStudentInCourse()
        {
            string answer = "";
            bool add = false;
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            while (answer != "exit" && add == false)
            {
                Console.WriteLine("Type the name of the course you want to remove a student. Type <<exit>> to leave.");
                answer = Console.ReadLine();
                foreach (Course course in allcourses)
                {
                    if (course.name == answer)
                    {
                        course.DeleteStudentInWorkGroup();
                        add = true;
                    }
                }
                if (add != true && answer != "exit")
                {
                    Console.WriteLine("This course doesn't exist");
                }
            }
        }

        public void AllCourse()
        {
            foreach(Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
        }
        public void ViewCourseInfos()
        {
            string answer = "";
            bool add = false;
            foreach (Course course in allcourses)
            {
                Console.WriteLine(course.name);
            }
            while (answer != "exit" && add == false)
            {
                Console.WriteLine("Type the name of the course you want to see infos. Type <<exit>> to leave.");
                answer = Console.ReadLine();
                foreach (Course course in allcourses)
                {
                    if (course.name == answer)
                    {
                        course.ViewDatas(); ;
                        add = true;
                    }
                }
                if (add != true && answer != "exit")
                {
                    Console.WriteLine("This course doesn't exist");
                }
            }
        }
    }
}
