using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_Attendance//:IDatabase  // mandatory template for this file: firstname, surname, id, worgroupName, professor name, professor surname and then all string about attendance
    {
        public string filepath; //file path
        List<List<string>> data; //all data related to the file
        List<WorkGroup> wrkGroupList; //all students are supposed to be inside Workgroups. this is used only on the first creation of the file, when the course is created
        public string courseName;

        public Database_Attendance(string _name, string _filepath, List<WorkGroup> _wrkGroupList) //this constructor is to create the file when creating a new course
        {                                                                                    //from an administrator profile
            this.courseName = _name;
            this.wrkGroupList = _wrkGroupList;
            this.filepath = _filepath;
            this.data = Infos();
            WriteInCsv();
        }
        public Database_Attendance(string _name, string _filepath)//this constructeur suits better when the csv file already exist.
        {
            this.courseName = _name;
            this.filepath = _filepath;
            this.wrkGroupList = new List<WorkGroup>();
            this.data = Infos();
            WriteInCsv();
        }

        public List<List<string>> Infos()  //firstname surname id worgroup marks
        {
            List<List<string>> final = new List<List<string>>();
            if (File.Exists(filepath))  //if the file exist, the datas are copied from the file.
            {
                string[] lines = File.ReadAllLines(filepath);
                for (int i = 0; i < lines.Length; i++)
                {
                    List<string> temporary = new List<string>();
                    temporary.Clear();
                    string[] columns = lines[i].Split(';');
                    foreach (string element in columns)
                    {
                        temporary.Add(element);
                    }
                    final.Add(temporary);
                }
                return final;
            }
            else //if the file doesn't exist, it creates the .csv file with all the students thar are assigned to this course, adding names, ID and their workgroup .
            {
                foreach (WorkGroup group in wrkGroupList)
                {
                    foreach (Student student in group.members)
                    {
                        List<string> temporary = new List<string>();
                        temporary.Clear();
                        temporary.Add(student.name); temporary.Add(student.surname); temporary.Add(student.ID); temporary.Add(group.name); temporary.Add(group.professor.name);
                        final.Add(temporary);
                    }
                }
                return final;
            }
            //result is an List of array of string. Each array contains all data on a person
        }

        public void WriteInCsv()
        {
            File.WriteAllText(filepath, "");
            StreamWriter file = new StreamWriter(filepath, true);
            for (int nblines = 0; nblines < data.Count(); nblines++)
            {
                string line = "";
                for (int i = 0; i < data[nblines].Count - 1; i++)
                {
                    line += data[nblines][i] + ";";
                }
                line += data[nblines][data[nblines].Count() - 1];
                file.WriteLine(line);
            }
            file.Close();
        }

        //method of this class:
        public List<List<string>> ReturnDatas()
        {
            data = Infos();
            return data;
        }

        //methods of this class

        public void AddStudentInWorkGroup(Student student, string groupName) //this method add a new person in the csv file
        {
            data = Infos();
            List<string> studInfo = new List<string>();
            studInfo.Add(student.name); studInfo.Add(student.surname); studInfo.Add(student.ID); studInfo.Add(groupName);
            data.Add(studInfo);
            WriteInCsv();
        }

        public void ModifyTeacherInWorkGroup(Teacher teacher, string groupName) //this method modify the professor in the csv file
        {
            data = Infos();
            List<string> studInfo = new List<string>();
            foreach (List<string> profile in data)
            {
                if (profile[3] == groupName)
                {
                    try
                    {
                        profile[4] = teacher.name; profile[4] = teacher.surname;
                    }
                    catch
                    {
                        profile.Add(teacher.name); profile.Add(teacher.surname);
                    }
                }
            }
            WriteInCsv();
        }

        public void DeleteStudentInWorkgroup(Student student) //this method delete the student from the CSV file
        {
            data = Infos();
            foreach (List<string> studinfo in data)
            {
                if (student.ID == studinfo[2])
                {
                    data.Remove(studinfo);
                }
            }
            WriteInCsv();
        }

        public bool IsThePersonAlreadyIn(string id) //this function give the idea if a person is in the file or not using id
        {
            data = Infos();
            bool found = false;
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i][2] == id)
                {
                    found = true;
                }
            }
            return found;
        }

        public bool IsThePersonAlreadyIn(string firstName, string surName) //this function give the idea if a person is in the file or not using names
        {
            data = Infos();
            bool found = false;
            for (int i = 0; i < data.Count(); i++)
            {
                if (data[i][0] == firstName && data[i][1] == surName)
                {
                    found = true;
                }
            }
            return found;
        }

        public int FindStudentInList(List<List<string>> list) //return the indexe of the researched student in the list
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

                    if (list[i][2] == firstName)
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
                    if (list[i][0].ToUpper() == firstName && list[i][1].ToUpper() == surName)
                    {
                        found = true;
                        return i;
                    }
                }
                //exception to handle here if the person is not found              
            }
            return -1; //return -1 if nothing is found.
        }

        public int FindStudentInList(List<List<string>> list, Student student)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i][2] == student.ID)
                {
                    return i;
                }
            }
            return -1;
        }
        public int FindStudentInList(List<List<string>> list, string id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i][2] == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<string> GetAttendance(string id)//will return a list of string containing the name of the course + Attendance reports
        {
            data = Infos();
            List<string> marks = new List<string>();
            marks.Add(courseName + " : ");
            int line = FindStudentInList(data,id);
            if (line != -1)
            {
                try //if the student has attendance, there are added
                {
                    for (int i = 6; i < data[line].Count; i++) //if there are attendance, they are displayed
                    {
                        marks.Add(data[line][i]);
                    }
                    return marks;
                }
                catch
                {
                    Console.WriteLine("the student has no attendance report in this course");
                    return marks = null;
                }
            }
            Console.WriteLine("the student is not in this course");
            return marks = null;
        }

        //this methode is the one used
        public void ModifAttendance() //this method will enable attendance modification
        {
            data = Infos(); //actualize with the data inside the csv file
            int line = FindStudentInList(data); //this function give the index of the researched student in the list.
            if (line != -1)
            {
                try
                {
                    if (data[line][4] != null && data[line][5] != null)
                    {
                        string answer = "add";
                        while (answer != "exit") //it is the menu of the function
                        {
                            Console.Write("attendance report of " + data[line][0] + " " + data[line][1] + " : ");
                            bool markToModify = false; //will indicates if there is mark available
                            try
                            {
                                for (int i = 6; i < data[line].Count; i++) //if there are reports, they are displayed
                                {
                                    Console.WriteLine(data[line][i] + " ");
                                }
                                markToModify = true;
                            }
                            catch
                            {
                                Console.WriteLine("the student has no attendance report");
                            }
                            Console.WriteLine();
                            Console.WriteLine("type: <<add>> to add a report to the student. or type <<modify>> to modify a report. Or type <<remove>> to remove a report. type <<exit>> to leave");
                            answer = Console.ReadLine();
                            if (answer == "add")
                            {
                                string answer2 = "";
                                Console.WriteLine("type the new attendance report you want to add. Please mark the date and time");
                                bool correct = true;
                                answer2 = Console.ReadLine();
                                if (correct == true) //if the report is correct, the report is added
                                {
                                    data[line].Add(answer2);
                                    Console.WriteLine("the attendance report has been hadded");
                                    WriteInCsv();
                                    data = Infos();
                                }
                                else
                                {
                                    Console.WriteLine("the attendance report has not been hadded");
                                }
                            }

                            if (answer == "modify")
                            {
                                if (markToModify == true) //only applies if there is a report to modify
                                {
                                    string answer2 = "";
                                    Console.WriteLine("type the index of report you want to modify. ex:1,2,3,...");
                                    answer2 = Console.ReadLine();
                                    Console.WriteLine("type the new report");
                                    string answer3 = Console.ReadLine();
                                    int index = 0;
                                    try
                                    {
                                        index = Convert.ToInt32(answer2); //try to convert the answers to verify if they are correct                               
                                        data[line][index + 5] = answer3; //try to replace the data. this verify if the index given exist maks start at index 5 in csv
                                        WriteInCsv();
                                        data = Infos();
                                    }
                                    catch
                                    {
                                        Console.WriteLine("the index or the new report is not correct");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("there is no mark to modify");
                                }
                            }

                            if (answer == "remove")
                            {
                                if (markToModify == true)//only applies if there is mark to delete
                                {
                                    string answer2 = "";
                                    Console.WriteLine("type the index of the report you want to remove. ex:1,2,3,...");
                                    answer2 = Console.ReadLine();
                                    int index = 0;
                                    try
                                    {
                                        index = Convert.ToInt32(answer2); //try to convert the answers to verify if they are correct 
                                        data[line].RemoveAt(5 + index);
                                        Console.WriteLine("the report has been removed");
                                        WriteInCsv();
                                        data = Infos();
                                    }
                                    catch
                                    {
                                        Console.WriteLine("the index is not correct");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("there is no attendance report to modify");
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("the student need to have an assigned professor to manage its marks");
                }
                
            }
            Console.WriteLine();
            WriteInCsv(); //save modifications in the csv
        }

        public void ModifAttendance(Student student) //this method will enable attendance modification
        {
            data = Infos(); //actualize with the data inside the csv file
            int line = FindStudentInList(data,student); //this function give the index of the researched student in the list.
            if (line != -1)
            {
                string answer = "add";
                while (answer != "exit") //it is the menu of the function
                {
                    Console.Write("attendance report of " + data[line][0] + data[line][1] + " : ");
                    bool markToModify = false; //will indicates if there is mark available
                    try
                    {
                        for (int i = 6; i < data[line].Count; i++) //if there are marks, they are displayed
                        {
                            Console.WriteLine(data[line][i] + " ");
                        }
                        markToModify = true;
                    }
                    catch
                    {
                        Console.WriteLine("the student has no attendance report");
                    }
                    Console.WriteLine();
                    Console.WriteLine("type: <<add>> to add a report to the student. or type <<modify>> to modify a report. Or type <<remove>> to remove a report. type <<exit>> to leave");
                    answer = Console.ReadLine();
                    if (answer == "add")
                    {
                        string answer2 = "";
                        Console.WriteLine("type the new attendance report you want to add. Please mark the date and time");
                        answer2 = Console.ReadLine();
                        data[line].Add(answer2);
                        Console.WriteLine("the attendance report has been hadded");
                        WriteInCsv();
                        data = Infos();
                    }

                    if (answer == "modify")
                    {
                        if (markToModify == true) //only applies if there is a report to modify
                        {
                            string answer2 = "";
                            Console.WriteLine("type the index of report you want to modify. ex:1,2,3,...");
                            answer2 = Console.ReadLine();
                            Console.WriteLine("type the new report");
                            string answer3 = Console.ReadLine();
                            int index = 0;
                            try
                            {
                                index = Convert.ToInt32(answer2); //try to convert the answers to verify if they are correct                               
                                data[line][index + 5] = answer3; //try to replace the data. this verify if the index given exist maks start at index 5 in csv
                                WriteInCsv();
                                data = Infos();
                            }
                            catch
                            {
                                Console.WriteLine("the index or the new report is not correct");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no mark to modify");
                        }
                    }

                    if (answer == "remove")
                    {
                        if (markToModify == true)//only applies if there is mark to delete
                        {
                            string answer2 = "";
                            Console.WriteLine("type the index of the report you want to remove. ex:1,2,3,...");
                            answer2 = Console.ReadLine();
                            int index = 0;
                            try
                            {
                                index = Convert.ToInt32(answer2); //try to convert the answers to verify if they are correct 
                                data[line].RemoveAt(5 + index);
                                Console.WriteLine("the report has been removed");
                                WriteInCsv();
                                data = Infos();
                            }
                            catch
                            {
                                Console.WriteLine("the index is not correct");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no attendance report to modify");
                        }
                    }
                }
            }
            WriteInCsv(); //save modifications in the csv
        }
    }
}
