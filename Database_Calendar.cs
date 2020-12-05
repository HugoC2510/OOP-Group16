using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_Calendar
    {
        public string filepath; //file path
        List<List<string>> data; //all data related to the file

        public Database_Calendar(string _filepath)//mandatory template for this file : course then date
        {
            this.filepath = _filepath;
            this.data = Infos();
            WriteInCsv();
        }

        public List<List<string>> Infos()  //firstname surname id worgroup marks
        {
            List<List<string>> final = new List<List<string>>();
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
            
            
            //result is an List of array of string. Each array contains all data on a person
        }
        public void WriteInCsv() // write all the modifications in the file
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
        public void AddDate(string course)//add a date in a course calendar
        {
            data = Infos();
            int index = FindLine(course);
            Console.WriteLine("Which date do you want to add in this course ? : ");
            string date = Console.ReadLine();
            bool validation = IsValid(date);
            while (validation == false)
            {
                Console.WriteLine("Please write a valid date (month + space + day) : ");
                date = Console.ReadLine();
                validation = IsValid(date);
            }
           
            data.ElementAt(index).Add(date);
            WriteInCsv();
        }
        public void ModifyDate(string course)//modify a date picked by the user
        {
            data = Infos();
            int index = FindLine(course);
            Console.WriteLine("Which date do you want to modify in this course ? (pick the number of the date) : ");
            for(int i=1;i<data.ElementAt(index).Count();i++)
            {
                Console.Write(i + " : " );
                DisplayDate(data.ElementAt(index).ElementAt(i));
                Console.WriteLine();
            }
            
            int choice = Convert.ToInt32(Console.ReadLine());
            while(choice<1 || choice>data.ElementAt(index).Count())
            {
                Console.WriteLine("Please write a possible choice : ");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Which date do you want to put at this place ? (correct writing : month + space + day)  ");
            string newDate = Console.ReadLine();
            bool validation = IsValid(newDate);
            while(validation==false)
            {
                Console.WriteLine("Please write a valid date (month + space + day) : ");
                newDate = Console.ReadLine();
                validation = IsValid(newDate);
            }
            
            data.ElementAt(index)[choice] = newDate;
            
            WriteInCsv();
        }
        public int FindLine(string course)//return the line of a course in the file
        {
            int answer = -1;
            int index = 0;
            data = Infos();
            foreach(List<string> l in data)
            {
                if(l.ElementAt(0).ToUpper()==course.ToUpper())
                {
                    answer = index;
                }
                index++;
            }
            return answer;
        }
        public bool IsValid(string date) //check if a date is correctly written
        {
            bool answer = true;
            char tab = date[2];
            if(tab==';'||tab=='/')
            {
                return false;
            }
            else
            {
                char[] separate = { '/', ' ' };
                
                if (date.Length != 5)
                {
                    answer = false;
                }
                string[] datesplit = date.Split(separate);
                int month = Convert.ToInt32(datesplit[0]);
                int day = Convert.ToInt32(datesplit[1]);
                if (day <= 0 || day > 31)
                {
                    answer = false;
                }
                if (month <= 0 || month > 12)
                {
                    answer = false;
                }
            }
            
            return answer;
        }
        
        public void DeleteDate(string course)//delete a date in a course calendar
        {
            data = Infos();
            int index=FindLine(course);           
            for (int i =1; i< data.ElementAt(index).Count(); i++)
            {
                Console.Write(i + " : " );
                DisplayDate(data.ElementAt(index).ElementAt(i));
                Console.WriteLine();
            }
            Console.WriteLine("Which date of exam you want to delete (month + space + day) ?");
            int newIndex = Convert.ToInt32(Console.ReadLine());
            while(newIndex<1 || newIndex > data.ElementAt(index).Count())
            {
                Console.WriteLine("Write a correct index of a exam date please");
                newIndex = Convert.ToInt32(Console.ReadLine());
            }           
            data.ElementAt(index).RemoveAt(newIndex);

            WriteInCsv();
        }

        public bool FindDate(string course)//check if a date already exists in a course calendar
        {
            data = Infos();
            Console.WriteLine("type the date that you want to check in the calendar ( template: month in numeric + space + date in numeric) ");
            string date = Console.ReadLine();
            bool validation = IsValid(date);
            while (validation == false)
            {
                Console.WriteLine("Please write a valid date (month + space + day) : ");
                date = Console.ReadLine();
                validation = IsValid(date);
            }
            bool numeric = false;
            int index = FindLine(course);
            for (int i = 1; i < data.ElementAt(index).Count(); i++)
            {
                if (date.ToUpper() == data.ElementAt(index)[i].ToUpper())
                {
                    numeric = true;
                    
                }
                
            

            }
            if (numeric==true)
            {
                Console.WriteLine("The date is in the calendar");
            }
            if(numeric==false)
            {
                Console.WriteLine("The date is not in the calendar");
            }
            
            
            return numeric;
        }

        public void DisplayCalendar()
        {
           
            for (int i = 0; i < data.Count(); i++)
            {
                for (int j = 0; j < data.ElementAt(i).Count(); j++)
                {
                    if (j > 0)
                    {
                        Console.Write(", Exam date : ");
                        DisplayDate(data[i][j]);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(data[i][j] + " ");
                    }
                    
                    
                    
                }
                Console.WriteLine();
            }
        }

        public void DisplayDate(string date) // show all the dates in the same way despite having them written in different ways
        {
            for (int i=0; i<date.Length;i++)
            {
                if(i==2)
                {
                    Console.Write("/");
                }
                else
                Console.Write(date[i]);
            }
        }
        public string AskCourse()
        {
           
            Console.WriteLine("What course do you want this action for ?");
            Console.WriteLine("1) English");
            Console.WriteLine("2) OOP");
            Console.WriteLine("3) Deutsh");
            Console.WriteLine("4) french");
            int answer = Convert.ToInt32(Console.ReadLine());
            while(answer<1 || answer>4)
            {
                Console.WriteLine("Please write a correct answer : ");
                answer = Convert.ToInt32(Console.ReadLine());
            }
            string final = "";
            if(answer==1)
            {
                final = "english";
            }
            if(answer==2)
            {
                final = "oop";
            }
            if(answer==3)
            {
                final = "deutsh";
            }
            if(answer==4)
            {
                final = "french";
            }
            return final;
        }
    }
}
