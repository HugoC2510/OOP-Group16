using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectVersion2
{
    public class Database_Login   //this class and its file is very important as every person's account is saved there.  finished on 4/11
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
        public  void ShowSortedFile()//the admin makes his choice and the function returns the database sorted as wanted
        {
            Console.WriteLine("How do you want your database sorted ? ");
            Console.WriteLine("Select your choice : ");
            Console.WriteLine("1) by name");
            Console.WriteLine("2) by surname");
            Console.WriteLine("3) by email");
            Console.WriteLine("4) by id");
            Console.WriteLine("5) by age");
            Console.WriteLine("6) if you want to see all the people of one gender");
            Console.WriteLine("7) if you want to see all the people of one status");
            Console.WriteLine("8) by last added");
            string answer = Console.ReadLine().ToUpper();
            switch (answer)
            {
                case "1":
                    List<Person>res=SortFile("1");
                    ShowAllPerson(res);
                    break;
                case "2":
                    List<Person> res2 = SortFile("2");
                    ShowAllPerson(res2);
                    break;
                case "3":
                    List<Person> res3 = SortFile("3");
                    ShowAllPerson(res3);
                    break;
                case "4":
                    List<Person> res4 = SortFile("4");
                    ShowAllPerson(res4);
                    break;
                case "5":
                    List<Person> res5 = SortFile("5");
                    ShowAllPerson(res5);
                    break;
                case "6":
                    List<Person> res6 = SortFile("6");
                    ShowAllPerson(res6);
                    break;
                case "7":
                    List<Person> res7 = SortFile("7");
                    ShowAllPerson(res7);
                    break;
                case "8":
                    List<Person> res8 = SortFile("8");
                    ShowAllPerson(res8);
                    break;
            }

        }
        public List<Person> EnumToList(IEnumerable<Person> enu)
        {
            List<Person> res = new List<Person>();
            foreach(Person p in enu)
            {
                res.Add(p);
            }
            return res;
        }

        public List<Person> SortFile(string choice)//sort the persons depending on the choice of the admin
        {
            int column = Convert.ToInt32(choice)-1;
            List<Person> all = AllPeople();
            List<Person> partial = new List<Person>();
            if(column==0)
            {
                
                IEnumerable<Person> res0=all.OrderBy(x => x.name);
                partial = EnumToList(res0);
                return partial;
            }
            if (column == 1)
            {
                IEnumerable<Person> res0 = all.OrderBy(x => x.surname);
                partial = EnumToList(res0);
                return partial;
            }
            if (column == 2)
            {
                IEnumerable<Person> res0 = all.OrderBy(x => x.email);
                partial = EnumToList(res0);
                return partial;
            }
            if(column==3)
            {
                IEnumerable<Person> res0 = all.OrderBy(x => x.ID);
                partial = EnumToList(res0);
                return partial;
            }
            if(column==4)
            {
                IEnumerable<Person> res0 = all.OrderBy(x => x.age);
                partial = EnumToList(res0);
                return partial;
            }
            if(column==5)
            {
                Console.WriteLine("You want to see the members of what gender ? (Select M or F)");
                string answer = Console.ReadLine().ToUpper();
                while(answer!="M" && answer!="F")
                {
                    Console.WriteLine("Select a correct answer please : ");
                    answer = Console.ReadLine();
                }
                if(answer=="M")
                {
                    foreach(Person p in all)
                    {
                        if(p.sex=="male")
                        {
                            partial.Add(p);
                        }
                    }
                }
                if (answer == "F")
                {
                    foreach (Person p in all)
                    {
                        if (p.sex == "female")
                        {
                            partial.Add(p);
                        }
                    }
                }
                return partial;
            }
            if(column==7)
            {
                return all;
            }
            else
            {
                Console.WriteLine("You want to see the members of what status ? (Select ADMINISTRATOR, PROFESSOR or STUDENT)");
                string answer = Console.ReadLine().ToUpper();
                while (answer != "ADMINISTRATOR" && answer != "PROFESSOR" && answer!="STUDENT")
                {
                    Console.WriteLine("Select a correct answer please : ");
                    answer = Console.ReadLine().ToUpper();
                }
                if (answer == "ADMINISTRATOR")
                {
                    foreach (Person p in all)
                    {
                        if (p.status.ToUpper() == "ADMINISTRATOR")
                        {
                            partial.Add(p);
                        }
                    }
                }
                if (answer == "STUDENT")
                {
                    foreach (Person p in all)
                    {
                        if (p.status.ToUpper() == "STUDENT")
                        {
                            partial.Add(p);
                        }
                    }
                }
                if (answer == "PROFESSOR")
                {
                    foreach (Person p in all)
                    {
                        if (p.status.ToUpper() == "PROFESSOR")
                        {
                            partial.Add(p);
                        }
                    }
                }
                return partial;

            }
        }
        public void ShowAllPerson(List<Person> pers)//give a quick description of each person in the database
        {
            foreach(Person p in pers)
            {
                Console.WriteLine("Name : " + p.name + "; Surname : " + p.surname + "; ID : " + p.ID + "; Age : " + p.age + "; Status : " + p.status
                    + "; Gender : " + p.sex+"; Mail :" +p.email);
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
            bool correct = false;
            string answer = "";
            while (correct != true)
            {
                Console.WriteLine("Choose the status of this account:");
                Console.WriteLine();
                Console.WriteLine("1) for a student account");
                Console.WriteLine("2) for a professeur account");
                Console.WriteLine("3) for an administrator account");
                answer = "";
                answer=Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        answer = "student";
                        correct = true;
                        break;
                    case "2":
                        answer = "professor";
                        correct = true;
                        break;
                    case "3":
                        answer = "administrator";
                        correct = true;
                        break;
                    default:
                        Console.WriteLine("incorrect answer");
                        break;
                }
            }            
            newAccount.Add(answer);
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
            if (newAccount[3] == "student")
            {
                try
                {
                    Student student = new Student(newAccount[0], newAccount[1], Convert.ToInt32(newAccount[7]), newAccount[6], newAccount[2], newAccount[8], newAccount[4], newAccount[5]);
                }
                catch
                {

                }
            }
            WriteInCsv();
            data = Infos();
            Console.WriteLine("new profile created!");
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

        public Student ReturnLastPerson()
        {
            data = Infos();
            if (data[data.Count() - 1][3] == "student")
            {
                string name = data[data.Count() - 1][0];
                string surname = data[data.Count() - 1][1];
                string mail = data[data.Count() - 1][2];
                string id = data[data.Count() - 1][4];
                string password = data[data.Count() - 1][5];
                string sex = data[data.Count() - 1][6];
                int age = Convert.ToInt32(data[data.Count() - 1][7]);
                string phone = data[data.Count() - 1][8];
                Student student = new Student(name, surname, age, sex, mail, phone, id, password);
                return student;
            }
            else
            {
                return null;
            }
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
            try
            {
                Console.WriteLine("Current informations of " + data.ElementAt(line)[0] + " " + data.ElementAt(line)[1] + " ID: " + data.ElementAt(line)[4] + " :");
                Console.WriteLine("Firsname: " + data.ElementAt(line)[0] + "  Surname: " + data.ElementAt(line)[1] + " Mail: " + data.ElementAt(line)[2]);
                Console.WriteLine("Status: " + data.ElementAt(line)[3] + " ID: " + data.ElementAt(line)[4] + " Password: " + data.ElementAt(line)[5] + " sexe: " + data.ElementAt(line)[6]);
                Console.WriteLine("Age: " + data.ElementAt(line)[7] + " Phonenumber: " + data.ElementAt(line)[8]);
                Console.WriteLine();
                Console.WriteLine("You have deleted the access to this profile. this person will not be able to log in again.");
                Console.WriteLine("Please, note that if the person is a student and if he has paid all the due fees, you have to manually");
                Console.WriteLine("remove him from the fees database, note its id to find him faster. Also, note that you may have to remove");
                Console.WriteLine("him from every courses he belong too.");
                data.RemoveAt(line);
                WriteInCsv();
                Console.WriteLine("Account removed");
            }
            catch
            {
                Console.WriteLine("This account does no longer exist");
            }
            
            
            Console.Read();
            
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
        public void ChangePassword(string id)//ables the user to change his password
        {
            //Console.WriteLine("Rewrite your ID please : ");
            //string res = Console.ReadLine();
            data = Infos();
            int line = InformationLine(id);
            Person p =Program.GetPerson(id);
            Console.WriteLine("You want to change your password ");
            Console.WriteLine("Please write your previous password : ");
            string answer = Console.ReadLine();
            while (answer != p.password)
            {
                Console.WriteLine("It's not your password, please try again : ");
                answer = Console.ReadLine();
            }
            Console.WriteLine("Your new password must contain at least 6 characters.");
            Console.WriteLine("Write your new password : ");
            string pass = Console.ReadLine();
            while (pass.Length < 6)
            {
                Console.WriteLine("Your password doesn't contain enough characters, please try again :");
                pass = Console.ReadLine();
            }
            Console.WriteLine("Confirm your new password : ");
            string confirm = Console.ReadLine();
            while (confirm != pass)
            {
                Console.WriteLine("The confirmation has failed, please try again : ");
                confirm = Console.ReadLine();
            }
            p.password = confirm;
            data.ElementAt(line)[5]=confirm;
            WriteInCsv();
            Console.WriteLine("Your new password has been saved");
        }
    }
}