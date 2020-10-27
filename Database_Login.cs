using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_Group_13_Week_1_2
{
    public class Database_Login : IDatabase
    {
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
                string[] columns = lines[i].Split(',');
                foreach(string element in columns)
                {
                    temporary.Add(element);
                }
                final.Add(temporary);
            }
            return final;     //result is an List of array of string. Each array contains all data on a person
        }

        public void ShowFile() //The purpose of this method is to show all informations on every people in the file
        {
            for(int i=0;i<data.Count;i++)
            {
                for(int j=0;j<data.ElementAt(i).Count;j++)
                {
                    Console.Write(data.ElementAt(i).ElementAt(j) + "; ");
                }
                Console.WriteLine(" ");
            }
        }
        //public List<string[]> CopyFile(List<string[]> origin)
        //{
        //    List<string[]> newFile = new List<string[]>();
        //    for(int i=0;i<origin.Count;i++)
        //    {
        //        string[] tab = new string[origin.ElementAt(i).Length];
        //        for(int j=0;j<origin.ElementAt(i).Length;j++)
        //        {
        //            tab[i] = origin.ElementAt(i).ElementAt(j);
        //        }
        //        newFile.Add(tab);
        //    }
        //    return newFile;
        //}

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
            data.Add(newAccount);
            WriteInCsv();
            data = Infos();
        }

        public int infoColumn(string word)  //to modify if there is more information to check
        {
            if(word.ToUpper()=="NAME")
            {
                return 0;
            }
            if(word.ToUpper()=="SURNAME")
            {
                return 1;
            }
            if (word.ToUpper() == "MAIL")
            {
                return 2;
            }
            if(word.ToUpper()=="STATUS")
            {
                return 3;
            }
            if(word.ToUpper()=="ID")
            {
                return 4;
            }
            else
            {
                return 5;
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
            string word="";
            while(word.ToUpper() != "FINISH")
            {
                Console.WriteLine("Current informations of " + data.ElementAt(line)[0] + " " + data.ElementAt(line)[1] + " ID: " + data.ElementAt(line)[2]);
                Console.WriteLine("Firsname: " + data.ElementAt(line)[0] + "  Surname: " + data.ElementAt(line)[1] + " ID: " + data.ElementAt(line)[2]);
                Console.WriteLine("email: " + data.ElementAt(line)[3] + " Status: " + data.ElementAt(line)[4]);
                Console.WriteLine("What information do you want to modify ? ");
                Console.WriteLine("NAME, " + "SURNAME, " + "MAIL, " + "STATUS " + "or ID");//to modify if there is more information to check
                Console.WriteLine("Type: Finish, to end modifications");
                word = Console.ReadLine();
                int choice = infoColumn(word.ToUpper());      //exception to handle here: if the typing is not correct or doesn't correspond to a choice
                Console.WriteLine("Type modification :");
                string modification = Console.ReadLine();
                data.ElementAt(line)[choice] = modification;
            }            
            WriteInCsv();
        }

        public int InformationLine(string[] names) //this method enable to find the line containing the datas of one person using its name to find it
        {
            data = Infos();
            int res = -1;  //if nothing is found, it returns -1
            for (int i=0;i<data.Count;i++)
            {
                if(data.ElementAt(i)[0].ToUpper()==names[0].ToUpper() && data.ElementAt(i)[1].ToUpper() == names[1].ToUpper()) 
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
            for (int i = 0; i < data.Count; i++)
            {
                if (data.ElementAt(i)[2].ToUpper() == number)
                {
                    res = i;
                }
            }
            return res;
        }

        public void RemoveInformation() //cette fonction permet de supprimer un utilisateur
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
            data.RemoveAt(line);
        }

        public void WriteInCsv()
        {
            File.WriteAllText(filepath,"");
            StreamWriter fileWriteLine =new StreamWriter(filepath, true);
            for(int nblines = 0; nblines <data.Count; nblines++)
            {
                string line = "";
                for (int i = 0; i < data[nblines].Count -1; i++)
                {                   
                    line += data[nblines][i] + ",";
                }
                line += data[nblines][data[nblines].Count()];
                fileWriteLine.WriteLine(line);
            }           
        }    
    }  
}
