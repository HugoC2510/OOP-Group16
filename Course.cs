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
        public Database_Attendance dataFileAttendance;
        public string yearGrade;

        public Course(string _name,string yeargrade)  //this constructor is used when it is the first time a course is created
        {
            this.name = _name;
            this.yearGrade = yeargrade;
            this.allGroups = new List<WorkGroup>();
            this.dataFileMarks = CreateCsvFileMarks(name);
            this.dataFileAttendance = CreateCsvFileAttendance(name);
            AddWrkGroupToCourse();
            AddStudentIntoWorkGroupOfaCourse();          
        }
        public Course(Database_Marks datafileMark, Database_Attendance datafileAttendance) //this constructor is used when the course is recreated from csv file
        {
            allGroups = RebuildFromFiles(datafileMark);
            this.name = datafileMark.courseName;
            this.dataFileAttendance = datafileAttendance;
        }
        public Course(string name, string yeargrade, string filepath1,string filepath2) //this constructor is used when the course is recreated from csv file
        {
            this.yearGrade = yeargrade;
            this.dataFileAttendance = new Database_Attendance(name, filepath2);
            this.dataFileMarks = new Database_Marks(name, filepath1);
            allGroups = RebuildFromFiles(dataFileMarks);
            this.name = dataFileMarks.courseName;
        }
        //public Database_Marks CreateCsvFileMarks(string courseName)
        //{
        //    Console.WriteLine("type the string path of the new file. Don't type .csv at the end, it is automatic");
        //    string path = Console.ReadLine();
        //    dataFileMarks = new Database_Marks(name + "dataFileMarks", path+".csv");
        //    Console.WriteLine();
        //    return dataFileMarks;
        //}
        public Database_Marks CreateCsvFileMarks(string courseName)
        {           
            dataFileMarks = new Database_Marks(courseName,courseName + "_dataFileMarks.csv");
            Console.WriteLine();
            return dataFileMarks;
        }
        public Database_Attendance CreateCsvFileAttendance(string courseName)
        {          
            dataFileAttendance = new Database_Attendance(courseName,courseName+"_dataFileAttendance.csv");
            Console.WriteLine();
            return dataFileAttendance;
        }

        public void AddWrkGroupToCourse() //this method allow the creation of workgroup inside a course
        {
            Console.WriteLine("In order to create the group, you must add at least one student in a workgroup");
            Console.WriteLine("Note that if there is no student added to this group after its creation, it won't be saved. ");
            Console.WriteLine("type the name of the workGroup: ");
            string name = Console.ReadLine();
            WorkGroup group = new WorkGroup(name);
            allGroups.Add(group);
            Console.WriteLine();
        }
        

        public void AddStudentIntoWorkGroupOfaCourse() //this method will add a student into a workgroup and update the data file.This method will be called in administrator
        {
            bool workGroupExist = false;
            foreach(WorkGroup group in allGroups)
            {
                workGroupExist = true;
            }
            if(workGroupExist == true)
            {
                Person person = Program.GetPerson();  //call of the function to find a person and verify if it is a student
                if (person is Student)
                {
                    Student student = person as Student;
                    Console.WriteLine();
                    if (dataFileMarks.IsThePersonAlreadyIn(student.ID) == false && dataFileAttendance.IsThePersonAlreadyIn(student.ID) == false) //we want to verify if the student is already in a group or not
                    {
                        Console.WriteLine("status of the workGroup in this course: " + name);
                        foreach (WorkGroup group in allGroups)
                        {
                            try
                            {
                                Console.WriteLine(group.name + " " + "there is  " + group.members.Count + " students in this group the professor is: " + group.professor.name + " " + group.professor.surname);
                            }
                            catch
                            {
                                Console.WriteLine(group.name + " " + "there is  " + group.members.Count + " students in this group ");
                            }
                        }
                        bool correctName = false;
                        string info = "";
                        while (correctName == false && info != "exit")
                        {
                            Console.WriteLine("In wich group add the student? type the name of the group or type: exit");
                            info = Console.ReadLine();
                            foreach (WorkGroup group in allGroups)
                            {
                                if (group.name == info)
                                {
                                    correctName = true;
                                    group.members.Add(student);
                                    dataFileMarks.AddStudentInWorkGroup(student, info); //add the student in the csv file with its workgroup name
                                    dataFileAttendance.AddStudentInWorkGroup(student, info); //add the student in the csv file with its workgroup name
                                    Console.WriteLine("the student has been hadded to the workGroup " + group.name);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }                                
                }
                else
                {
                    Console.WriteLine("The person you have chosen is not a student");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("there is no workgroup in this course. Create a workgroup before to add student in it.");
                Console.WriteLine();
            }
            allGroups = RebuildFromFiles(dataFileMarks);//actualizes the changes in the list of workgroup
        }

        public void ModifyTeacherIntoWorkGroupOfaCourse() //this method will enable modification of the teacher into a workgroup and  update the data file
        {
            Console.WriteLine();
            Console.WriteLine("status of the workGroups in this course: " + name);
            foreach (WorkGroup group in allGroups)
            {
                try
                {
                    Console.WriteLine(group.name + " " + "there is  " + group.members.Count + " students in this group. The professor is: " + group.professor.name + " " + group.professor.surname);
                }
                catch
                {
                    Console.WriteLine(group.name + " " + "there is  " + group.members.Count + " students. there is no professor");
                }
            }           
            bool correctName = false;
            string info = "";
            while (correctName == false && info != "exit")
            {
                Console.WriteLine("in wich group modify or add a teacher? type the name of the group or type: <<exit>>");
                info = Console.ReadLine();
                foreach (WorkGroup group in allGroups)
                {
                    if (group.name == info)
                    {
                        correctName = true;
                        Console.WriteLine("find the person in order to modify or add a teacher ");
                        Person person = Program.GetPerson();
                        if(person is Teacher)
                        {
                            Teacher teacher = person as Teacher;
                            group.professor = teacher;
                            dataFileMarks.ModifyTeacherInWorkGroup(teacher, info);//update on csv file
                            dataFileAttendance.ModifyTeacherInWorkGroup(teacher, info);
                            Console.WriteLine("the teacher has been changed in the workGroup " + group.name);
                        }
                        else
                        {
                            Console.WriteLine("the person you have chosen is not a Teacher.");
                        }                      
                    }
                }
            }
            Console.WriteLine();
            allGroups = RebuildFromFiles(dataFileMarks);
        }

        public void DeleteStudentInWorkGroup()  //function to rewrite as the changes are now applied by the rebuilcourse()
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
                                
                                dataFileMarks.DeleteStudentInWorkgroup(student);
                                dataFileAttendance.DeleteStudentInWorkgroup(student);
                                found = true;
                                Console.WriteLine("The student has been removed"); //the student should be removed from the csv now.
                            }
                        }
                    }
                    this.allGroups = RebuildFromFiles(this.dataFileMarks);
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
                                dataFileMarks.DeleteStudentInWorkgroup(student);
                                dataFileAttendance.DeleteStudentInWorkgroup(student);
                                found = true;
                                Console.WriteLine("The student has been removed");
                            }
                        }
                    }
                    this.allGroups = RebuildFromFiles(this.dataFileMarks);
                }
                if(found == false)
                {
                    Console.WriteLine("student not found");
                }
            }
            Console.WriteLine();
            allGroups = RebuildFromFiles(dataFileMarks);
        }

        public bool StudentPresentInCourse(Student student)
        {
            this.allGroups = RebuildFromFiles(this.dataFileMarks);
            bool present = false;
            foreach(WorkGroup wkgp in allGroups)
            {
                foreach(Student stud in wkgp.members)
                {
                    if(student.EqualPerson(stud) == true)
                    {
                        present = true;
                    }
                }
            }
            return present;
        }

        public bool TeacherPresentInCourse(Teacher teacher)
        {
            this.allGroups = RebuildFromFiles(this.dataFileMarks);
            bool present = false;
            try
            {
                foreach (WorkGroup wkgp in allGroups)
                {
                    if (wkgp.professor.EqualPerson(teacher) == true)
                    {
                        present = true;
                        return present;
                    }
                }
            }
            catch
            {

            }
            return present;
        }
        public WorkGroup ReturnWorkgroup(Teacher teacher)
        {
            if (TeacherPresentInCourse(teacher) == true)
            {
                foreach (WorkGroup group in allGroups)
                {
                    if (group.professor.EqualPerson(teacher) == true)
                    {
                        return group;
                    }
                }
            }
            return null;
        }

        public WorkGroup ReturnWorkgroup(Student stud)
        {
            if (StudentPresentInCourse(stud) == true)
            {
                foreach(WorkGroup group in allGroups)
                {
                    foreach(Student student in group.members)
                    {
                        if(student.EqualPerson(stud) == true)
                        {
                            return group;
                        }
                    }
                }
            }
            return null;
        }
        public void ViewDatas()
        {
            Console.WriteLine();
            allGroups = RebuildFromFiles(dataFileMarks);
            Console.WriteLine("organisation of the course : " + name);          
            foreach (WorkGroup group in allGroups)
            {
                Console.WriteLine();
                try
                {
                    Console.WriteLine(group.name + ": " + "there is  " + group.members.Count + " students in this group. The professor is: " + group.professor.name + " " + group.professor.surname);
                    Console.WriteLine("List of the students:");
                    foreach (Student student in group.members)
                    {
                        Console.WriteLine("      " + student.ID + " " + student.surname + " "+ student.name );
                    }
                }
                catch
                {
                    Console.WriteLine(group.name + ": " + "there is  " + group.members.Count + " students. there is no professor");
                    Console.WriteLine("List of the students:");
                    foreach (Student student in group.members)
                    {
                        Console.WriteLine("      " + student.ID + " " + student.surname +" "+ student.name);
                    }
                }
            }
        }

        public void ModifyMark()
        {
            dataFileMarks.ModifMarks();
        }
        public void ModifyMark(Student stud)
        {
            dataFileMarks.ModifMarks(stud);
        }
        public void ModifyAttendance()
        {
            dataFileAttendance.ModifAttendance();
        }
        public void ModifyAttendance(Student stud)
        {
            dataFileAttendance.ModifAttendance(stud);
        }

        public List<WorkGroup> RebuildFromFiles(Database_Marks marks) //this function enables to rebuilt a course from a csv file.
        {
            List<List<string>> data = marks.ReturnDatas();
            List<string> workGroupName = new List<string>(); //this list is temporary and used in order to create the correct wkgp and not 2x the same one
            List<WorkGroup> everyWorkGroup = new List<WorkGroup>();
            foreach(List<string> personInfo in data)
            {
                if (workGroupName.Contains(personInfo[3]) == false)
                {
                    workGroupName.Add(personInfo[3]);
                    WorkGroup group = new WorkGroup(personInfo[3]);
                    try
                    {
                        if (personInfo[4] != null && personInfo[5] != null)
                        {
                            Teacher teacher = Program.GetPerson(personInfo[4], personInfo[5]) as Teacher;
                            group.professor = teacher;  //at this point, the workgroup and its teacher is generated
                        }
                    }
                    catch
                    {

                    }                  
                    everyWorkGroup.Add(group);
                }
                Person person = Program.GetPerson(personInfo[2]);
                {
                    if (person is Student)
                    {
                        Student student = person as Student;
                        foreach(WorkGroup wkgp in everyWorkGroup)
                        {
                            if(wkgp.name == personInfo[3])
                            {
                                wkgp.members.Add(student);   //at this point the student is added inside its workgroup
                                student.group.Add(wkgp);
                                student.courseList.Add(name); //the course is added to the student course list
                            }
                        }
                    }                  
                }
            }
            return everyWorkGroup;   //should get the list of all WorkGroup in it            
        }
    }
}
