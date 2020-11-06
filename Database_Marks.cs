﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_Marks//:IDatabase  // mandatory template for this file: firstname, surname, id, worgroupName, professor name, professor surname and then marks
    {
        private string filepath; //file path
        List<List<string>> data; //all data related to the file
        List<WorkGroup> wrkGroupList; //all students are supposed to be inside Workgroups. this is used only on the first creation of the file, when the course is created
        public string course; //course name
        string name;

        public Database_Marks(string _name, string _filepath, List<WorkGroup> _wrkGroupList) //this constructor is to create the file when creating a new course
        {                                                                                    //from an administrator profile
            this.name = _name;
            this.wrkGroupList = _wrkGroupList;
            this.filepath = _filepath;
            this.data = Infos();
            WriteInCsv();
        }
        public Database_Marks(string _name, string _filepath)//this constructeur suits better when the csv file already exist.
        {
            this.name = _name;
            this.filepath = _filepath;
            this.data = Infos();
            WriteInCsv();
        }

        //basic method of this class to write and save a csv

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

        //methods of this class

        public void AddStudentInWorkGroup(Student student, string groupName) //this method add a new person in the csv file
        {
            data = Infos();
            List<string> studInfo = new List<string>();
            studInfo.Add(student.name); studInfo.Add(student.surname); studInfo.Add(student.ID); studInfo.Add(groupName);
            WriteInCsv();
        }

        public void ModifyTeacherInWorkGroup(Teacher teacher, string groupName) //this method add a new person in the csv file
        {
            data = Infos();
            List<string> studInfo = new List<string>();
            foreach(List<string>profile in data)
            {
                if(profile[3]== groupName)
                {
                    profile[4] = teacher.name; profile[4] = teacher.surname;
                }
            }           
            WriteInCsv();
        }

        public void DeleteStudentInWorkgroup(Student student) //this method delete the student from the CSV file
        {
            data = Infos();
            foreach(List<string> studinfo in data)
            {
                if (student.ID == studinfo[2])
                {
                    data.Remove(studinfo);
                }
            }
            WriteInCsv();
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

        public void ModifMarks() //this method will enable mark modification
        {
            data = Infos(); //actualize with the data inside the csv file
            int line = FindStudentInList(data); //this function give the index of the researched student in the list.
            if (line != -1)
            {
                Console.WriteLine("Marks of : " + data[line][0] + data[line][1]+" : ");
                bool markToModify = false; //will indicates if there is mark available
                try
                {
                    for (int i = 6; i < data[line].Count; i++) //if there are marks, they are displayed
                    {
                        Console.Write(data[line][i] + " ");
                    }
                    markToModify = true;
                }
                catch
                {
                    Console.WriteLine("the student has no mark");
                }
                string answer = "";
                while(answer != "add" || answer!= "modify"|| answer != "exit" || answer!="remove") //it is the menu of the function
                {
                    Console.WriteLine("type: <<add>> to add a mark to the student. or type <<modify>> to modify a mark. type <<exit>> to leave");
                    answer = Console.ReadLine();
                    if (answer == "add")
                    {
                        string answer2 = "";
                        Console.WriteLine("type the new mark to add");
                        double mark=0;
                        bool correct = true;
                        answer2 = Console.ReadLine();
                        try
                        {
                            mark = Convert.ToDouble(answer2); //the conversion needs to beverified
                        }
                        catch
                        {
                            Console.WriteLine("it is not a valid syntax");
                            correct = false;
                        }
                        if (correct == true) //if the mark is correct, hthe mark is added
                        {
                            data[line].Add(Convert.ToString(mark));
                            Console.WriteLine("the mark has been hadded");
                        }
                        else
                        {
                            Console.WriteLine("the mark has not been hadded");
                        }            
                    }

                    if (answer == "modify")
                    {
                        if (markToModify == true) //only applies if there is mark to modify
                        {
                            string answer2 = "";
                            Console.WriteLine("type the index of mark you want to modify. ex:1,2,3,...");                          
                            answer2 = Console.ReadLine();
                            Console.WriteLine("type the new mark");
                            string answer3 = Console.ReadLine();
                            int index = 0;
                            double mark = 0;
                            try
                            {
                                index=Convert.ToInt32(answer2); //try to convert the answers to verify if they are correct
                                mark = Convert.ToDouble(answer3);
                                data[line][index + 5] = answer3; //try to replace the data. this verify if the index given exist maks start at index 5 in csv
                            }
                            catch
                            {
                                Console.WriteLine("the index or the new mark is not correct");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no mark to modify");
                        }                     
                    }

                    if(answer == "remove")
                    {
                        if (markToModify == true)//only applies if there is mark to delete
                        {
                            string answer2 = "";
                            Console.WriteLine("type the index of mark you want to remove. ex:1,2,3,...");
                            answer2 = Console.ReadLine();
                            int index = 0;
                            try
                            {
                                index = Convert.ToInt32(answer2); //try to convert the answers to verify if they are correct 
                                data[line].RemoveAt(5 + index);
                                Console.WriteLine("Mark has been removed");
                            }
                            catch
                            {
                                Console.WriteLine("the index is not correct");
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no mark to modify");
                        }
                    }
                }                
            }
            WriteInCsv(); //save modifications in the csv
        }

        

        

    }
}