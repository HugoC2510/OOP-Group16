using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("sssssnot found");
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
                Console.WriteLine("bbbbnot found");
            }
            return null;
        }


        static void Main(string[] args) //contains verifie un type reference  il faut trouver une autre soltuion
        {
            Database_Login database_Login = new Database_Login("DataFileLogIn1.csv");
            List<Person> all = database_Login.AllPeople();
            Console.WriteLine("type the name of the course you want to create");
            Course course = new Course(Console.ReadLine());

            course.AddWrkGroupToCourse();
            course.AddStudentIntoWorkGroupOfaCourse();
            course.ModifyTeacherIntoWorkGroupOfaCourse();
            course.ModifyMark();
            course.ModifyAttendance();
            Console.Read();
            //for next time: look when you add mark but ther is no professor
        }
    }
}
