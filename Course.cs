using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class Course
    {
        public List<int> marks;
        public List<WorkGroup> allGroups;
        public List<Teacher> teachers;
        public string name;

        public Course(string _name,List<int> _marks, List<WorkGroup> _allGroups, List<Teacher> _teachers)
        {
            this.marks = _marks;
            this.allGroups = _allGroups;
            this.teachers = _teachers;
            this.name = _name;
        }
        public void ModifyName()//this method ables somebody to modify the name of the course
        {
            Console.WriteLine("The actual name of this course is : " + name);
            Console.WriteLine("What name do you want for this course ? ");
            this.name = Console.ReadLine();
        }
        public void AddMarks()//this method ables a teacher to add a mark
        {
            Console.WriteLine("What is your first name ? ");
            string firstName = Console.ReadLine().ToUpper();
            Console.WriteLine("What is your last name ? ");
            string name = Console.ReadLine().ToUpper();
            bool exist=teachers.Exists(x => x.name.ToUpper() == name); //we check if the teacher is able to add a mark to this course
            if(exist==true)
            {
                Teacher prof = teachers.Find(x => x.name.ToUpper() == name);
                Console.WriteLine("What is your password ? "); //we ask the teacher to write his password
                string password = Console.ReadLine();
                int tries = 2;
                while(password!=prof.password && tries>0)
                {
                    Console.WriteLine("The password is incorrect, try again (you still have " + tries + " tries)");
                    password = Console.ReadLine();
                    tries--;
                }
                if(password==prof.password)//if it's okay, the mark can be added
                {
                    Console.WriteLine("What mark do you want to add ? ");
                    int mark = Convert.ToInt32(Console.ReadLine());
                    marks.Add(mark);
                }
                else
                {
                    Console.WriteLine("Sorry, try later");
                }
            }
            else
            {
                Console.WriteLine("Sorry, you are not able to modify marks for this course");
            }
        }
        
    }
}
