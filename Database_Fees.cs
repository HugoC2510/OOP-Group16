using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_Fees  //template of this datafile: name, surname, id, total fees, due fees, due date, payment method
    {
        //made by: 
        //23168 Hugo Camps
        //23175 Albert De Watrigant
        //23196 Aurelien Delicourt
        //23172 Jean-Marc Hanna
        //22842 Julien Msika
        //22830 Lorenzo Mendes

        public string filepath; //file path      
        List<List<string>> data; //all data related to the file
        List<Person> allstudents;// all the students. used when it is the first time the file is created;

        public Database_Fees()
        {
            this.filepath = "Database_Fees.csv";
            this.data = Infos();
        }

        public Database_Fees(List<Person> studs) //this constructor is used when it is the first time the file is generated
        {
            this.filepath = "Database_Fees.csv";
            this.allstudents = studs;
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
                foreach(Person stud in allstudents)
                {
                    if(stud is Student)
                    {
                        Student student = stud as Student;
                        List<string> temporary = new List<string>();
                        temporary.Clear();
                        temporary.Add(student.name); temporary.Add(student.surname); temporary.Add(student.ID); temporary.Add("10000"); temporary.Add("10000");
                        temporary.Add("12/31"); temporary.Add("One time");
                        final.Add(temporary);
                    }
                    
                }
                return final;
            }
            //result is a List of array of string. Each array contains all data on a person
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

        //method of the class:
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

        public void DeleteStudent(string id)
        {
            data = Infos();
            int index = FindStudentInList(data, id);
            if (index != -1)
            {
                data.RemoveAt(index);
                Console.WriteLine("Student deleted from the fee's file.");
            }
            WriteInCsv();
        }

        public string ReturnFeesInfos(string id)
        {
            data = Infos();
            int index = FindStudentInList(data, id);
            string line = "";
            if (index != 1)
            {
                line = "Total ammount: " + data[index][3] + " Remaining ammount : " + data[index][4] + " Due date : " + data[index][5] + " Payment method : " + data[index][6];              
            }
            return line;
        }
        public void AddStudent(Student stud)
        {
            data = Infos();
            List<string> newstudent = new List<string>();
            newstudent.Add(stud.name);
            newstudent.Add(stud.surname);
            newstudent.Add(stud.ID);
            newstudent.Add("10000");
            newstudent.Add("10000");
            newstudent.Add("12/31");
            newstudent.Add("One time");
            data.Add(newstudent);
            WriteInCsv();
        }
        public void ModifyDatas()
        {
            this.data = Infos();
            int index = FindStudentInList(data);
            if (index != -1)
            {
                string answer = "";
                Console.WriteLine();
                while (answer != "exit" && answer!="5")
                {
                    try
                    {
                        data = Infos();
                        Console.WriteLine("Payment status of : " + data[index][2] + " " + data[index][0] + " " + data[index][1]);
                        Console.WriteLine("total fees :" + data[index][3] + " Remaining due fees: " + data[index][4] + " Due date: " + data[index][5] + " Payment method : " + data[index][6]);
                        Console.WriteLine();
                    }
                    catch
                    {

                    }
                    
                    Console.WriteLine("Type the number:");
                    Console.WriteLine("1) To modify total due fees.");
                    Console.WriteLine("2) To modify the remaining fees.");
                    Console.WriteLine("3) To modify the due date");
                    Console.WriteLine("4) To modify the payment method.");
                    Console.WriteLine("5) To remove a student from this database.");
                    Console.WriteLine("type <<exit>> to leave");
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "1":
                            Console.WriteLine();
                            Console.WriteLine("Type the new ammount:");
                            string answer2 = Console.ReadLine();
                            bool numeric = true;
                            try
                            {
                                int.Parse(answer2);
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect syntax!");
                                numeric = false;
                            }
                            if (numeric == true)
                            {
                                try
                                {
                                    data[index][3] = answer2;
                                    Console.WriteLine("Modification done");
                                }
                                catch
                                {
                                    Console.WriteLine("Modification not done");
                                }
                            }
                            break;

                        case "2":
                            Console.WriteLine();
                            Console.WriteLine("Type the new ammount:");
                            string answer3 = Console.ReadLine();
                            bool numeric2 = true;
                            try
                            {
                                int.Parse(answer3);
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect syntax!");
                                numeric = false;
                            }
                            if (numeric2 == true)
                            {
                                if(Convert.ToInt32(answer3) <= Convert.ToInt32(data[index][3]))
                                {
                                    try
                                    {
                                        data[index][3] = answer3;
                                        Console.WriteLine("Modification done");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Modification not done");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Modification not done");
                                    Console.WriteLine("The remaining fees can not be higher than the initial total ammount!");
                                }                                
                            }
                            break;

                        case "3":
                            Console.WriteLine();
                            Console.WriteLine("Type the new date: type the month first (ie : 1~12");
                            string answer4 = Console.ReadLine();
                            Console.WriteLine("Type the new date: type the day (ie : 1~31");
                            string answer5 = Console.ReadLine();
                            bool numeric3 = true;
                            try
                            {
                                int.Parse(answer4);
                                int.Parse(answer5);
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect syntax!");
                                numeric = false;
                            }
                            if(numeric3 == true)  //making the answer correspond to a true calendar
                            {
                                bool correctDate = false;
                                if(int.Parse(answer4)==1 || int.Parse(answer4) == 3|| int.Parse(answer4) == 3 || int.Parse(answer4) == 5 || int.Parse(answer4) == 7 || int.Parse(answer4) == 8 || int.Parse(answer4) == 10 || int.Parse(answer4) == 12)
                                {
                                    if (int.Parse(answer5) <= 31)
                                    {
                                        correctDate = true;
                                    }
                                }
                                if(int.Parse(answer4) == 2)
                                {
                                    if (int.Parse(answer5) <= 28)
                                    {
                                        correctDate = true;
                                    }
                                }
                                if (int.Parse(answer4) == 4 || int.Parse(answer4) == 6 || int.Parse(answer4) == 9 || int.Parse(answer4) == 11)
                                {
                                    if (int.Parse(answer5) <= 30)
                                    {
                                        correctDate = true;
                                    }
                                }
                                if (correctDate == true)
                                {
                                    string newDate = Convert.ToString(answer4) + "/" + Convert.ToString(answer5);
                                    try
                                    {
                                        data[index][5] = newDate;
                                        Console.WriteLine("Modification done");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Modification not done");
                                    }
                                }
                            }
                            break;

                        case "4":
                            
                            Console.WriteLine("Select the payment method :");
                            Console.WriteLine("1) One Time.");
                            Console.WriteLine("2) Semestrial.");
                            string answer6 = Console.ReadLine();
                            switch (answer6)
                            {
                                case "1":
                                    try
                                    {
                                        data[index][6] = "One Time";
                                        Console.WriteLine("Modification done");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Modification not done");
                                    }
                                    break;
                                case "2":
                                    try
                                    {
                                        data[index][6] = "Semestrial";
                                        Console.WriteLine("Modification done");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Modification not done");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Type correct answer");
                                    Console.WriteLine();
                                    break;

                            }
                            break;
                        case "5":
                            DeleteStudent(data[index][2]);
                            Console.WriteLine("The student has been removed.");
                            Console.WriteLine();
                            break;

                        case "exit":
                            Console.WriteLine();
                            break;

                        default:
                            Console.WriteLine("Type correct answer");
                            Console.WriteLine();
                            break;

                    }
                    WriteInCsv();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("This ID is not in the datafile. You typed it wrong or this ID doesn't correspond to a student");
                Console.WriteLine();
            }
        }
    }
}
