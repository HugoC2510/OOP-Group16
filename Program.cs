using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    class Program
    {
        public static Person GetPerson()
        {
            Console.WriteLine();
            Database_Login database_Login = new Database_Login("DataFileLogIn1.csv");
            List<Person> all = database_Login.AllPeople();
            bool found = false;
            string firstName = "";
            string surName = "";
            while( found!= true && firstName!= "EXIT" && surName!= "EXIT")
            {
                Console.WriteLine("To find the person you want, type Firstname or ID. Type <<exit>> to leave");
                firstName = Console.ReadLine().ToUpper();
                bool numeric = true;
                try
                {
                    int.Parse(firstName);
                }
                catch
                {
                    numeric = false;
                }              
                if (numeric == true)
                {
                    foreach (Person person in all)
                    {
                        if (person.ID == firstName && found==false) 
                        {
                            found = true;
                            if (person.status.ToUpper() == "STUDENT")
                            {
                                return person as Student;
                            }
                            if (person.status.ToUpper() == "PROFESSOR")
                            {
                                return person as Teacher;
                            }
                            if (person.status.ToUpper() == "ADMINISTRATOR")
                            {
                                return person as Administrator;
                            }
                        }
                    }
                }
                if(numeric==false && firstName!="EXIT")
                {
                    Console.WriteLine("Enter Surname. Type <<exit>> to leave");
                    surName = Console.ReadLine().ToUpper();
                    foreach (Person person in all)
                    {
                        if (person.name.ToUpper() == firstName && person.surname.ToUpper() == surName)
                        {
                            found = true;
                            if (person.status.ToUpper() == "STUDENT")
                            {
                                return person as Student;
                            }
                            if (person.status.ToUpper() == "PROFESSOR")
                            {
                                return person as Teacher;
                            }
                            if (person.status.ToUpper() == "ADMINISTRATOR")
                            {
                                return person as Administrator;
                            }
                        }
                    }                              
                }
                if(found == false)
                {

                    Console.WriteLine("This person has not been found, try again");
                }
            }
            return null;  
        }
        public static Person GetPerson(List<Person> all)
        {
            Console.WriteLine();
            bool found = false;
            string firstName = "";
            string surName = "";
            while (found != true && firstName != "EXIT" && surName != "EXIT")
            {
                Console.WriteLine("To find the person you want, type Firstname or ID. Type <<exit>> to leave");
                firstName = Console.ReadLine().ToUpper();
                bool numeric = true;
                try
                {
                    int.Parse(firstName);
                }
                catch
                {
                    numeric = false;
                }
                if (numeric == true)
                {
                    foreach (Person person in all)
                    {
                        if (person.ID == firstName && found == false)
                        {
                            found = true;
                            if (person.status.ToUpper() == "STUDENT")
                            {
                                return person as Student;
                            }
                            if (person.status.ToUpper() == "PROFESSOR")
                            {
                                return person as Teacher;
                            }
                            if (person.status.ToUpper() == "ADMINISTRATOR")
                            {
                                return person as Administrator;
                            }
                        }
                    }
                }
                if (numeric == false && firstName != "EXIT")
                {
                    Console.WriteLine("Enter Surname. Type <<exit>> to leave");
                    surName = Console.ReadLine().ToUpper();
                    foreach (Person person in all)
                    {
                        if (person.name.ToUpper() == firstName && person.surname.ToUpper() == surName)
                        {
                            found = true;
                            if (person.status.ToUpper() == "STUDENT")
                            {
                                return person as Student;
                            }
                            if (person.status.ToUpper() == "PROFESSOR")
                            {
                                return person as Teacher;
                            }
                            if (person.status.ToUpper() == "ADMINISTRATOR")
                            {
                                return person as Administrator;
                            }
                        }
                    }
                }
                if (found == false)
                {

                    Console.WriteLine("This person has not been found, try again");
                }
            }
            return null;
        }
        public static Person GetPerson(string ID)
        {
            Database_Login database_Login = new Database_Login("DataFileLogIn1.csv");
            List<Person> all = database_Login.AllPeople();
            bool found = false;
            foreach(Person person in all)
            {
                if (person.ID == ID)
                {
                    found = true;
                    if (person.status.ToUpper() == "STUDENT")
                    {
                        return person as Student;
                    }
                    if (person.status.ToUpper() == "PROFESSOR")
                    {
                        return person as Teacher;
                    }
                    if (person.status.ToUpper() == "ADMINISTRATOR")
                    {
                        return person as Administrator;
                    }
                }
            }
            if(found == false)
            {
                Console.WriteLine("not found");
            }
            return null;
        }

        public static Person GetPerson(string firstName,string surName)
        {
            Database_Login database_Login = new Database_Login("DataFileLogIn1.csv");
            List<Person> all = database_Login.AllPeople();
            bool found = false;
            foreach (Person person in all)
            {
                if (person.name == firstName && person.surname == surName && found==false)
                {
                    found = true;
                    if (person.status.ToUpper() == "STUDENT")
                    {
                        return person as Student;
                    }
                    if (person.status.ToUpper() == "PROFESSOR")
                    {
                        return person as Teacher;
                    }
                    if (person.status.ToUpper() == "ADMINISTRATOR")
                    {
                        return person as Administrator;
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("not found");
            }
            return null;
        }

        public static void UpdateEveryPersonStatus(List<Person> all, List<Course> courseList, Database_Login database, Database_CourseList dataCourseList, Database_Fees database_Fees)
        {
            foreach(Person person in all)
            {
                if (person is Student)
                {
                    Student stud = person as Student;
                    stud.feeStatus=database_Fees.ReturnFeesInfos(stud.ID);
                    foreach (Course course in courseList)
                    {
                        if (course.StudentPresentInCourse(stud) == true)
                        {
                            stud.courseList.Add(course.name);
                            if (course.ReturnWorkgroup(stud) != null)
                            {
                                stud.group.Add(course.ReturnWorkgroup(stud));
                            }
                            if (course.dataFileAttendance.GetAttendance(stud.ID) != null)
                            {
                                stud.attendance.Add(course.dataFileAttendance.GetAttendance(stud.ID));
                            }
                            if (course.dataFileMarks.GetMarks(stud.ID) != null)
                            {
                                stud.marks.Add(course.dataFileMarks.GetMarks(stud.ID));
                            }
                            //adding all datas from the courses into student's attributes.
                        }
                    }
                }
                if(person is Teacher)
                {
                    Teacher teacher = person as Teacher;
                    foreach (Course course in courseList)
                    {
                        if (course.TeacherPresentInCourse(teacher) == true)
                        {
                            teacher.courses.Add(course);
                            if (course.ReturnWorkgroup(teacher) != null)
                            {
                                teacher.groups.Add(course.ReturnWorkgroup(teacher));
                            }
                        }
                    }
                }
                if(person is Administrator)
                {
                    Administrator admin = person as Administrator;
                    admin.database_Login = database;
                    admin.allcourses = courseList;
                    admin.allpersons = all;
                    admin.database_CourseList = dataCourseList;
                    admin.database_Fees = database_Fees;
                }
            }
        }
        public static void Application(Database_Login database_Login, Database_CourseList database_CourseList, Database_Fees database_Fees)
        {
            List<Person> all = database_Login.AllPeople();
            List<Course> courses = database_CourseList.courseListe;
            UpdateEveryPersonStatus(all, courses, database_Login, database_CourseList, database_Fees);
            bool connexion = false;
            FirstDisplay();
            while (connexion == false)
            {
                BlackAndWhite();
                Console.WriteLine("Enter your email:");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                string answer = Console.ReadLine();
                Console.ResetColor();
                BlackAndWhite();
                Console.WriteLine("Enter your password");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                string answer2 = Console.ReadLine();
                Console.ResetColor();
                Console.WriteLine();
                foreach(Person person in all)
                {
                    if(person.email==answer && person.password==answer2)
                    {
                        connexion = true;
                        Console.WriteLine("Welcome to you portal " + person.name + " " + person.surname);
                        Console.WriteLine();
                        if (person.status.ToUpper() == "STUDENT")
                        {
                            Student student = person as Student;
                            string answerstud = "";
                            while(answerstud != "exit")
                            {
                                Console.WriteLine("Type the number:");
                                Console.WriteLine("1) To view your personal information.");
                                Console.WriteLine("2) To view your marks.");
                                Console.WriteLine("3) To view your Attendance report.");
                                Console.WriteLine("4) To view your finance.");
                                Console.WriteLine("5) To change your password.");
                                Console.WriteLine("type exit to leave");
                                Console.WriteLine();
                                answerstud=Console.ReadLine();
                                switch (answerstud)
                                {
                                    case "1":
                                        student.Tostring();
                                        Console.WriteLine();
                                        break;
                                    case "2":
                                        student.DisplayAllMarks();
                                        Console.WriteLine();
                                        break;
                                    case "3":
                                        student.DisplayAllAttendanceReport();
                                        Console.WriteLine();
                                        break;
                                    case "4":
                                        student.DisplayFeeStatues();
                                        break;
                                    case "5":
                                        database_Login.ChangePassword(student.ID);
                                        break;
                                    case "exit":
                                        Console.WriteLine();
                                        break;

                                    default:
                                        Console.WriteLine("Type correct answer");
                                        Console.WriteLine();
                                        break;

                                }
                            }
                        }
                        if(person.status.ToUpper() == "PROFESSOR")
                        {
                            Teacher teacher = person as Teacher;
                            string answerperson = "";
                            while (answerperson != "exit")
                            {
                                Console.WriteLine("Type the number:");
                                Console.WriteLine("1) To view your personal information.");
                                Console.WriteLine("2) To modify the marks of your student.");
                                Console.WriteLine("3) To modify the Attendance report of your students.");
                                Console.WriteLine("4) To change your password.");
                                Console.WriteLine("type exit to leave");
                                Console.WriteLine();
                                answerperson = Console.ReadLine();
                                switch (answerperson)
                                {
                                    case "1":
                                        teacher.Tostring();
                                        Console.WriteLine();
                                        break;
                                    case "2":
                                        teacher.ModifyMarksInCourses();
                                        Console.WriteLine();
                                        break;
                                    case "3":
                                        teacher.ModifyAttendanceInCourses();
                                        Console.WriteLine();
                                        break;
                                    case "4":
                                        database_Login.ChangePassword(teacher.ID);
                                        break;
                                    case "exit":
                                        Console.WriteLine();
                                        break;

                                    default:
                                        Console.WriteLine("Type correct answer");
                                        Console.WriteLine();
                                        break;

                                }
                            }
                        }
                        if(person.status.ToUpper() == "ADMINISTRATOR")
                        {
                            Administrator admin = person as Administrator;
                            string answerperson = "";
                            while (answerperson != "exit")
                            {
                                Console.WriteLine("Type the number:");
                                Console.WriteLine("1) To view your personal information.");
                                Console.WriteLine("2) To modify the marks of a student.");
                                Console.WriteLine("3) To modify the Attendance report of a student.");
                                Console.WriteLine("4) To add a new person to the organization");
                                Console.WriteLine("5) To modify a person's profile to the organization");
                                Console.WriteLine("6) To remove a person from the organization");
                                Console.WriteLine("7) To view someone's informations.");
                                Console.WriteLine("8) To Create a new course.");
                                Console.WriteLine("9) To add a workgroup to a course.");
                                Console.WriteLine("10) To add a student to a course;");
                                Console.WriteLine("11) To remove a student from a course.");
                                Console.WriteLine("12) To change the teacher in charge of a workgroup in a course.");
                                Console.WriteLine("13) To view all courses datas.");
                                Console.WriteLine("14) To manage the finance status of a student.");
                                Console.WriteLine("15) To see all the late assignements of a course.");
                                Console.WriteLine("16) To see informations about the members of the organization.");
                                Console.WriteLine("17) To change your password.");
                                Console.WriteLine("type exit to leave");
                                Console.WriteLine();
                                answerperson = Console.ReadLine();
                                switch (answerperson)
                                {
                                    case "1":
                                        admin.Tostring();
                                        Console.WriteLine();
                                        break;
                                    case "2":
                                        admin.ModifySomeonesMarks();
                                        Console.WriteLine();
                                        break;
                                    case "3":
                                        admin.ModifySomeonesAttendance();
                                        Console.WriteLine();
                                        break;
                                    case "4":
                                        admin.AddPersonInOrganization();
                                        Console.WriteLine();
                                        break;
                                    case "5":
                                        admin.ModifyProfile();
                                        Console.WriteLine();
                                        break;
                                    case "6":
                                        admin.RemovePersonFromOrganization();
                                        Console.WriteLine();
                                        break;
                                    case "7":
                                        admin.ShowProfile();
                                        Console.WriteLine();
                                        break;
                                    case "8":
                                        admin.CreateNewCourse();
                                        Console.WriteLine();
                                        break;
                                    case "9":
                                        admin.AddWorkGroupToACourse();
                                        Console.WriteLine();
                                        break;
                                    case "10":
                                        admin.AddPersonToACourse();
                                        Console.WriteLine();
                                        break;
                                    case "11":
                                        admin.RemoveStudentInCourse();
                                        Console.WriteLine();
                                        break;
                                    case "12":
                                        admin.ModifyTeacherInWorkGroup();
                                        Console.WriteLine();
                                        break;
                                    case "13":
                                        admin.ViewCourseInfos();
                                        Console.WriteLine();
                                        break;
                                    case "14":
                                        admin.database_Fees.ModifyDatas();
                                        break;
                                    case "15":
                                        
                                        Console.WriteLine("Which course do you want to check (choose between allemand, anglais, OOP and français) : ");
                                        
                                        string answ = Console.ReadLine().ToUpper();
                                        foreach(Course c in admin.allcourses)
                                        {
                                            if(c.name.ToUpper()==answ)
                                            {
                                                c.HowMuchMissed();
                                            }
                                        }
                                        break;
                                    case "16":
                                        database_Login.ShowSortedFile();
                                        break;
                                    case "17":
                                        database_Login.ChangePassword(admin.ID);
                                        break;
                                    case "exit":
                                        Console.WriteLine();
                                        break;
                                    default:
                                        Console.WriteLine("Type correct answer");
                                        Console.WriteLine();
                                        break;

                                }
                            }
                        }
                    }
                }
            }        
        }
        static void FirstDisplay()
        {
            GetSpaces(16);
            for (int i = 0; i < 80; i++)
            {
                
                Console.Write("*");

            }
            Console.WriteLine(" ");
            for(int i=0;i<4;i++)
            {
                GetSpaces(15);
                Console.Write("|");
                if(i==2)
                {
                    GetSpaces(30);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("WELCOME TO YOUR PORTAL");
                    Console.ResetColor();
                    GetSpaces(28);
                    Console.Write("|");
                    Console.WriteLine(" ");
                    GetSpaces(15);
                    Console.Write("|");
                }
                GetSpaces(80);
                Console.Write("|");
                Console.WriteLine(" ");
            }
            GetSpaces(16);
            for (int i = 0; i < 80; i++)
            {

                Console.Write("*");

            }
            for(int i=0;i<3;i++)
            {
                Console.WriteLine(" ");
            }
            GetSpaces(54);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("SIGN IN");
            Console.ResetColor();
            Console.WriteLine();

        }
        static void GetSpaces(int number)
        {
            for(int i=0;i<number;i++)
            {
                Console.Write(" ");
            }
        }
        static void BlackAndWhite()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void Main(string[] args) 
        {
            Database_Login database_Login = new Database_Login("DataFileLogIn1.csv");
            Database_CourseList database_CourseList = new Database_CourseList("Database_CourseList.csv");
            Database_Fees database_Fees = new Database_Fees();
            Console.WriteLine("files generated");
            Application(database_Login, database_CourseList, database_Fees);

            //Database_Fees database_Fees = new Database_Fees(database_Login.AllPeople());
            //database_Fees.ModifyDatas();
            
            //List<Person> all = new List<Person>();
            //all = database_Login.AllPeople();
            //List<Course> courses = new List<Course>();
            //courses = database_CourseList.courseListe;
            //Course cour = new Course("anglais", "A2");
            //cour.ModifyMark();
            //database_CourseList.AddACourse(cour);
            //foreach (Course course in courses)
            //{
            //    course.ViewDatas();
            //    //if (course.name == "anglais")
            //    //{
            //    //    course.ModifyTeacherIntoWorkGroupOfaCourse();
            //    //    course.ModifyMark();
            //    //}
            //}
            //Console.WriteLine("list person generated");
            //UpdateEveryPersonStatus(all, courses, database_Login);
            //Console.WriteLine("list person updated");
            //foreach (Person person in all)
            //{
            //    if(person is Student)
            //    {
            //        Student stud = person as Student;
            //        if (stud.ID == "2001")
            //        {
            //            stud.Tostring();
            //            foreach (string course in stud.courseList)
            //            {
            //                Console.WriteLine(course);
            //            }
            //            stud.DisplayAllMarks();
            //            stud.DisplayAllAttendanceReport();
            //        }
            //    }
            //    if(person is Teacher)
            //    {
            //        Teacher stud = person as Teacher;
            //        if (stud.ID == "2009")
            //        {
            //            stud.Tostring();
            //            foreach(Course course in stud.courses)
            //            {
            //                Console.WriteLine(course.name);
            //            }
            //            foreach(WorkGroup group in stud.groups)
            //            {
            //                Console.WriteLine(group.name);
            //            }
            //            //stud.ModifyMarksInCourses();
            //            stud.ModifyAttendanceInCourses();
            //        }
            //    }
                
            //}
            ////List<Course> courses = database_CourseList.courseListe;
            //foreach (Course course in courses)
            //{
            //    course.ViewDatas();
            //}
            //database_CourseList.DeleteACourse();
           

            //Console.WriteLine("type the name of the course you want to create");
            //Course course = new Course(Console.ReadLine(), "A2");
            //database_CourseList.AddACourse(course);
            ////Course course = new Course("allemand", "A2", "allemand_dataFileMarks.csv", "allemand_dataFileAttendance.csv");
            ////database_CourseList.AddACourse(course);
            ////course.ViewDatas();
            ////course.AddWrkGroupToCourse();
            //course.AddStudentIntoWorkGroupOfaCourse();
            //course.ModifyTeacherIntoWorkGroupOfaCourse();
            //course.ModifyMark();
            //course.ModifyAttendance();
            Console.Read();
            //for next time: put a function called mise a jour course list
        }
    }
}
