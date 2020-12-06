using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_CourseList //template: courseName,yearGrade,marks file,attendance file
    {
        //made by: 
        //23168 Hugo Camps
        //23175 Albert De Watrigant
        //23196 Aurelien Delicourt
        //23172 Jean-Marc Hanna
        //22842 Julien Msika
        //22830 Lorenzo Mendes

        private string filepath;
        List<List<string>> data;
        public List<Course> courseListe { get; set; }

        public Database_CourseList(string _filepath)//use this constructor when the file already exist
        {
            this.filepath = _filepath;
            this.data = Infos();
            this.courseListe = CreateAllCourse();
            WriteInCsv();
        }

        public Database_CourseList()//use this constructor when the file doesn't exist 
        {           
            this.filepath = "Database_CourseList.csv";
            this.data = Infos();
            this.courseListe = CreateAllCourse();
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
            else //if the file doesn't exist, it creates the .csv file but it is empty.
            {
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

        //method of the class

        public void AddACourse(Course course)
        {
            data = Infos();
            if(course.dataFileMarks.filepath!=null && course.dataFileAttendance.filepath!= null && course.name != null && course.yearGrade!=null)
            {
                List<string> newCourse = new List<string>();
                newCourse.Add(course.name); newCourse.Add(course.yearGrade); newCourse.Add(course.dataFileMarks.filepath); newCourse.Add(course.dataFileAttendance.filepath);
                data.Add(newCourse);
                WriteInCsv();
            }
        }

        public List<Course> CreateAllCourse()
        {
            data = Infos();
            List<Course> courses = new List<Course>();
            foreach (List<string> cours in data)
            {
                try
                {
                    Course course = new Course(cours[0],cours[1], cours[2], cours[3]);
                    courses.Add(course);
                }
                catch
                {

                }
            }
            return courses;
        }

        public void DeleteACourse() //this function will have to be modified if we unify the list of person
        {
            data = Infos();
            Console.WriteLine("type the name of the course you want to delete. type <<exit>> to cancel");
            string answer = "";
            bool delete = false;
            while(delete==false && answer != "exit")
            {
                answer= Console.ReadLine();
                int index = -1;
                for(int i=0; i<data.Count;i++)
                {
                    if (data[i][0] == answer)
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    data.RemoveAt(index);
                    WriteInCsv();
                    Console.WriteLine("removed from csv");
                }
                int index2 = -1;
                for (int i = 0; i < courseListe.Count; i++)
                {
                    if (courseListe[i].name == answer)
                    {
                        index2 = i; 
                    }
                }
                if (index2 != -1)
                {
                    File.Delete(courseListe[index2].dataFileMarks.filepath);
                    File.Delete(courseListe[index2].dataFileAttendance.filepath);
                    this.courseListe.RemoveAt(index2);
                    Console.WriteLine("The course has been fully removed");
                    delete = true;
                }
                if (delete == false)
                {
                    Console.WriteLine("there has been a problem with the course you typed. pls try again. ");
                }
                
                try
                {
                   
                }
                catch
                {
                    //Console.WriteLine("there has been a problem with the course you typed. pls try again. ");
                }
            }
                
        }

    }
}
