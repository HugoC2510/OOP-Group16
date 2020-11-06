using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class Course  //comment stocker la classe course et ses etudiants a long terme
    {
        public List<WorkGroup> allGroups;
        public string name;
        public Database_Marks dataFileMarks;

        public Course(string _name)
        {
            this.name = _name;
        }

        public void CreateCsvFile()
        {
            Console.WriteLine("type the string path of the new file");
            string path = Console.ReadLine();
            dataFileMarks = new Database_Marks(name + "dataFileMarks", path);
        }
        public void AddWrkGroupToCourse() //this method allow the creation of workgroup inside a course
        {
            Console.WriteLine("type the name of the workGroup: ");
            string name = Console.ReadLine();
            WorkGroup group = new WorkGroup(name);
            allGroups.Add(group);
        }
        
        public void AddStudentIntoWorkGroupOfaCourse(Student student) //this method will add a student into a workgroup and update the data file.This method will be called in administrator
        {
            Console.WriteLine("status of the workGroup in this course: " + name);
            foreach(WorkGroup group in allGroups)
            {
                Console.WriteLine(group.name + " "  + "there is  " + group.members.Count+ " students in this group the professor is: " + group.professor.name + " " + group.professor.surname);
            }  
            bool correctName = false;
            string info = "";
            while (correctName == false && info!="exit")
            {
                Console.WriteLine("in wich group add the student? type the name of the group or type: exit");
                info = Console.ReadLine();
                foreach (WorkGroup group in allGroups)
                {
                    if (group.name == info)
                    {
                        correctName = true;
                        group.members.Add(student);
                        dataFileMarks.AddStudentInWorkGroup(student, info); //add the student in the csv file with its workgroup name
                        Console.WriteLine("the student has been hadded to the workGroup " + group.name);
                    }
                }
            }          
        }

        public void ModifyTeacherIntoWorkGroupOfaCourse(Teacher teacher) //this method will add a student into a workgroup and  update the data file
        {
            Console.WriteLine("status of the workGroups in this course: " + name);
            foreach (WorkGroup group in allGroups)
            {
                Console.WriteLine(group.name + " " + "there is  " + group.members.Count + " students in this group the professor is: " + group.professor.name +" "+ group.professor.surname);
            }
            bool correctName = false;
            string info = "";
            while (correctName == false && info != "exit")
            {
                Console.WriteLine("in wich group modify the teacher? type the name of the group or type: exit");
                info = Console.ReadLine();
                foreach (WorkGroup group in allGroups)
                {
                    if (group.name == info)
                    {
                        correctName = true;
                        group.professor=teacher;
                        dataFileMarks.ModifyTeacherInWorkGroup(teacher, info);
                        Console.WriteLine("the teacher has been changed in the workGroup " + group.name);
                    }
                }
            }
        }

        public void DeleteStudentInWorkGroup()
        {
            string firstName = "";
            bool found = false;
            string surName = "";
            while(found==false && firstName!="exit" && surName!="exit")
            {
                Console.WriteLine("Who is the student you want to remove from this course? type his Firstname or his ID. type <<exit>> to leave");
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
                int line;
                if (numeric == true)
                {
                    foreach (WorkGroup group in allGroups) //we must also reemove the student from the workgroup list
                    {
                        foreach (Student student in group.members)
                        {
                            if (student.ID == firstName)
                            {
                                group.members.Remove(student);
                                dataFileMarks.DeleteStudentInWorkgroup(student);
                                found = true;
                                Console.WriteLine("The student has been removed"); //the student should be removed from the csv now.
                            }
                        }
                    }
                }
                else  
                {
                    Console.WriteLine("Enter Surname. Type leave to exit ");
                    surName = Console.ReadLine().ToUpper();
                    string[] names = new string[2];
                    names[0] = Convert.ToString(firstName); names[1] = surName;
                    foreach (WorkGroup group in allGroups) //we must also reemove the student from the workgroup list
                    {
                        foreach (Student student in group.members)
                        {
                            if (student.name == firstName && student.surname == surName)
                            {
                                group.members.Remove(student);
                                dataFileMarks.DeleteStudentInWorkgroup(student);
                                found = true;
                                Console.WriteLine("The student has been removed");
                            }
                        }
                    }
                }
                if(found == false)
                {
                    Console.WriteLine("student not found");
                }
            }           
        }

        public void ModifyMark()
        {
            dataFileMarks.ModifMarks();
        }
        
    }
}
