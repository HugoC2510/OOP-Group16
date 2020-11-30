using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_Login   //this class and its file is very important as every person's account is saved there.
    {                                        //finished on 4/11
        private string filepath;
        List<List<string>> data;
        public Database_Login(string _filepath)
        {
            this.filepath = _filepath;
            this.data = Infos();
        }

        public List<List<string>> Infos()  //The purpose of this method is to get full informations on every people in the file in a List that can be used further
        {
            string[] lines = System.IO.File.ReadAllLines(filepath);
            List<List<string>> final = new List<List<string>>();
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
            return final;     //result is an List of array of string. Each array contains all data on a person
        }
        public void ShowFile() //The purpose of this method is to show all informations on every people in the file
        {
            data = Infos();
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data.ElementAt(i).Count; j++)
                {
                    Console.Write(data.ElementAt(i).ElementAt(j) + "; ");
                }
                Console.WriteLine(" ");
            }
        }
 
        public void ShowInformation()//this method shows all information of one person in the database
        {
            data = Infos();
            Console.WriteLine("Who is the person you want to see information? : enter Firstname  or ID");
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
            if (numeric == true)
            {
                line = InformationLine(Convert.ToString(firstName));
            }
            else
            {
                Console.WriteLine("Enter Surname ");
                string surName = Console.ReadLine().ToUpper();
                string[] names = new string[2];
                names[0] = Convert.ToString(firstName); names[1] = surName;
                line = InformationLine(names);      //exception to handle here if the person is not found              
            }
            Console.WriteLine("Current informations of " + data.ElementAt(line)[0] + " " + data.ElementAt(line)[1] + " ID: " + data.ElementAt(line)[4] + " :");
            Console.WriteLine("Firsname: " + data.ElementAt(line)[0] + "  Surname: " + data.ElementAt(line)[1] + " Mail: " + data.ElementAt(line)[2]);
            Console.WriteLine("Status: " + data.ElementAt(line)[3] + " ID: " + data.ElementAt(line)[4] + " Password: " + data.ElementAt(line)[5] + " sexe: " + data.ElementAt(line)[6]);
            Console.WriteLine(" Age: " + data.ElementAt(line)[7] + " Phonenumber: " + data.ElementAt(line)[8]);
            Console.Read();
        }

        public void AddInformation() //The purpose of this method is to add informations of one person in the file, in order to create a profile for exemple
        {
            data = Infos();
            List<string> newAccount = new List<string>();
            Console.WriteLine("Name : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Surname : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Mail : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Status : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("ID : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Password : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Sexe : ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Age: ");
            newAccount.Add(Console.ReadLine());
            Console.WriteLine("Phone number : ");
            newAccount.Add(Console.ReadLine());
            data.Add(newAccount);
            WriteInCsv();
            data = Infos();
        }
        public int infoColumn(string word)  //to modify if there is more information to check
        {
            if (word.ToUpper() == "NAME")
            {
                return 0;
            }
            if (word.ToUpper() == "SURNAME")
            {
                return 1;
            }
            if (word.ToUpper() == "MAIL")
            {
                return 2;
            }
            if (word.ToUpper() == "STATUS")
            {
                return 3;
            }
            if (word.ToUpper() == "ID")
            {
                return 4;
            }
            if (word.ToUpper() == "PASSWORD")
            {
                return 5;
            }
            if (word.ToUpper() == "SEXE")
            {
                return 6;
            }
            if (word.ToUpper() == "AGE")
            {
                return 7;
            }
            if (word.ToUpper() == "PHONE NUMBER")
            {
                return 8;
            }
            else
            {
                return -1;
            }
        }
        public void ModifyFile() //This method is used to modifiy someone's informations
        {
            data = Infos();
            Console.WriteLine("Who is the person you want to modify information? : enter Firstname  or ID");
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
            if (numeric == true)
            {
                line = InformationLine(Convert.ToString(firstName));
            }
            else
            {
                Console.WriteLine("Enter Surname ");
                string surName = Console.ReadLine().ToUpper();
                string[] names = new string[2];
                names[0] = Convert.ToString(firstName); names[1] = surName;
                line = InformationLine(names);      //exception to handle here if the person is not found              
            }
            string word = "";
            while (word.ToUpper() != "FINISH")
            {
                Console.WriteLine("Current informations of " + data.ElementAt(line)[0] + " " + data.ElementAt(line)[1] + " ID: " + data.ElementAt(line)[4] + " :");
                Console.WriteLine("Firsname: " + data.ElementAt(line)[0] + "  Surname: " + data.ElementAt(line)[1] + " Mail: " + data.ElementAt(line)[2]);
                Console.WriteLine("Status: " + data.ElementAt(line)[3] + " ID: " + data.ElementAt(line)[4] + " Password: " + data.ElementAt(line)[5] + " sexe: " + data.ElementAt(line)[6]);
                Console.WriteLine(" Age: " + data.ElementAt(line)[7] + " Phonenumber: " + data.ElementAt(line)[8]);
                Console.WriteLine("What information do you want to modify ? ");
                Console.WriteLine("NAME, " + "SURNAME, " + "MAIL, " + "STATUS, " + "ID, " + "PASSWORD, " + "SEXE, " + "AGE, " + "or PHONE NUMBER");//to modify if there is more information to check
                Console.WriteLine("Type: FINISH, to end modifications");
                word = Console.ReadLine();
                if (word.ToUpper() != "FINISH")
                {
                    int choice = infoColumn(word.ToUpper());      //exception to handle here: if the typing is not correct or doesn't correspond to a choice
                    Console.WriteLine("Type modification :");
                    string modification = Console.ReadLine();
                    data.ElementAt(line)[choice] = modification;
                }
            }
            WriteInCsv();
        }

        public int InformationLine(string[] names) //this method enable to find the line containing the datas of one person using its name to find it
        {
            data = Infos();
            int res = -1;  //if nothing is found, it returns -1
            for (int i = 0; i < data.Count(); i++)
            {
                if (data.ElementAt(i)[0].ToUpper() == names[0].ToUpper() && data.ElementAt(i)[1].ToUpper() == names[1].ToUpper())
                {
                    res = i;
                }
            }
            return res;
        }
        public int InformationLine(string number) //this method enable to find the line containing the datas of one person using its ID to find it
        {

            data = Infos();
            int res = -1;  //if nothing is found, it returns -1            
            for (int i = 0; i < data.Count(); i++)
            {
                if (data.ElementAt(i)[4].ToUpper() == number)
                {
                    res = i;
                }
            }
            return res;
        }
        public void RemoveInformation() //this method enable the suppression of an user
        {
            data = Infos();
            Console.WriteLine("Which account do you want to remove ? Type Firstname or ID ");
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
            if (numeric == true)
            {
                line = InformationLine(Convert.ToString(firstName));
            }
            else
            {
                Console.WriteLine("Enter Surname ");
                string surName = Console.ReadLine().ToUpper();
                string[] names = new string[2];
                names[0] = Convert.ToString(firstName); names[1] = surName;
                line = InformationLine(names);      //exception to handle here if the person is not found              
            }
            Console.WriteLine("Current informations of " + data.ElementAt(line)[0] + " " + data.ElementAt(line)[1] + " ID: " + data.ElementAt(line)[4] + " :");
            Console.WriteLine("Firsname: " + data.ElementAt(line)[0] + "  Surname: " + data.ElementAt(line)[1] + " Mail: " + data.ElementAt(line)[2]);
            Console.WriteLine("Status: " + data.ElementAt(line)[3] + " ID: " + data.ElementAt(line)[4] + " Password: " + data.ElementAt(line)[5] + " sexe: " + data.ElementAt(line)[6]);
            Console.WriteLine("Age: " + data.ElementAt(line)[7] + " Phonenumber: " + data.ElementAt(line)[8]);
            Console.WriteLine();
            Console.WriteLine("Account removed");
            Console.Read();
            data.RemoveAt(line);
            WriteInCsv();
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

        public List<Person> AllPeople()
        {
            data = Infos();
            List<Person> allPeople = new List<Person>();
            foreach (List<string> l in data)
            {
                if (l.ElementAt(3).ToUpper() == "STUDENT")
                {
                    string name = l.ElementAt(0);
                    string surname = l.ElementAt(1);
                    string mail = l.ElementAt(2);
                    string id = l.ElementAt(4);
                    string password = l.ElementAt(5);
                    string sex = l.ElementAt(6);
                    int age = Convert.ToInt32(l.ElementAt(7));
                    string phone = l.ElementAt(8);
                    Student test = new Student(name, surname, age, sex, mail, phone, id, password);
                    allPeople.Add(test);
                }
                if (l.ElementAt(3).ToUpper() == "PROFESSOR")
                {
                    string name = l.ElementAt(0);
                    string surname = l.ElementAt(1);
                    string mail = l.ElementAt(2);
                    string id = l.ElementAt(4);
                    string password = l.ElementAt(5);
                    string sex = l.ElementAt(6);
                    int age = Convert.ToInt32(l.ElementAt(7));
                    string phone = l.ElementAt(8);
                    Teacher test = new Teacher(id, name, surname, age, sex, mail, phone, password);
                    allPeople.Add(test);
                }
                if (l.ElementAt(3).ToUpper() == "ADMINISTRATOR")
                {
                    string name = l.ElementAt(0);
                    string surname = l.ElementAt(1);
                    string mail = l.ElementAt(2);
                    string id = l.ElementAt(4);
                    string password = l.ElementAt(5);
                    string sex = l.ElementAt(6);
                    int age = Convert.ToInt32(l.ElementAt(7));
                    string phone = l.ElementAt(8);
                    Administrator test = new Administrator(id, name, surname, age, sex, mail, phone, password);
                    allPeople.Add(test);
                }
            }
            return allPeople;
        }

 

        //public List<Student> StudentGroup()
        //{
        //    List<List<string>> data = this.Infos();
        //    List<Student> answer = new List<Student>();
        //    foreach(List<string> l in data)
        //    {
        //        if (l.ElementAt(3).ToUpper() == "STUDENT") 
        //        {
        //            string name = l.ElementAt(0);
        //            string surname = l.ElementAt(1);
        //            string mail = l.ElementAt(2);
        //            string id = l.ElementAt(4);
        //            string password = l.ElementAt(5);
        //            char sex = Convert.ToChar(l.ElementAt(6));
        //            int age = Convert.ToInt32(l.ElementAt(7));
        //            string phone = l.ElementAt(8);
        //            Student test = new Student(name, surname, age, sex, mail, phone, id,password);
        //            answer.Add(test);
        //        }
        //    }
        //    return answer;
        //}
        //public List<Teacher> TeacherGroup()
        //{
        //    List<List<string>> data = this.Infos();
        //    List<Teacher> answer = new List<Teacher>();
        //    foreach (List<string> l in data)
        //    {
        //        if (l.ElementAt(3).ToUpper() == "TEACHER")
        //        {
        //            string name = l.ElementAt(0);
        //            string surname = l.ElementAt(1);
        //            string mail = l.ElementAt(2);
        //            string id = l.ElementAt(4);
        //            string password = l.ElementAt(5);
        //            char sex = Convert.ToChar(l.ElementAt(6));
        //            int age = Convert.ToInt32(l.ElementAt(7));
        //            string phone = l.ElementAt(8);
        //            Teacher test = new Teacher(id, name, surname, age, sex, mail, phone, password);
        //            answer.Add(test);
        //        }
        //    }
        //    return answer;
        //}
        //public List<Administrator> AdminGroup()
        //{
        //    List<List<string>> data = this.Infos();
        //    List<Administrator> answer = new List<Administrator>();
        //    foreach (List<string> l in data)
        //    {
        //        if (l.ElementAt(3).ToUpper() == "ADMINISTRATOR")
        //        {
        //            string name = l.ElementAt(0);
        //            string surname = l.ElementAt(1);
        //            string mail = l.ElementAt(2);
        //            string id = l.ElementAt(4);
        //            string password = l.ElementAt(5);
        //            char sex = Convert.ToChar(l.ElementAt(6));
        //            int age = Convert.ToInt32(l.ElementAt(7));
        //            string phone = l.ElementAt(8);
        //            Administrator test = new Administrator(id, name, surname, age, sex, mail, phone, password);
        //            answer.Add(test);
        //        }
        //    }
        //    return answer;
        //}

    }
}