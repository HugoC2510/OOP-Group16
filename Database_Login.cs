using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class Database_Login : IDatabase
    {
        private string filepath;
        public Database_Login(string _filepath)
        {
            this.filepath = _filepath;
        }
        
        public List<string[]> Infos() //creation of a List<string []> after the file
        {
            string[] lines = System.IO.File.ReadAllLines(filepath); //we read the file
            List<string []> final = new List<string[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');
                final.Add(columns); 
            }
            return final; //we return a list<string[]> that contains all the content of the file
        }
        public void ShowFile() //show in the console the file
        {
            List<string[]> file = this.Infos();
            for(int i=0;i<file.Count;i++)
            {
                for(int j=0;j<file.ElementAt(i).Length;j++)
                {
                    Console.Write(file.ElementAt(i).ElementAt(j) + "; ");
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
        public void AddInformation() //method that ables the adition of a new count
        {
            List<string[]> elements = this.Infos();
            string[] tab = new string[15];
            //we ask the user for each information he wants to add
            Console.WriteLine("Name : ");
            tab[0]= Console.ReadLine();
            Console.WriteLine("Surname : ");
            tab[1] = Console.ReadLine();
            Console.WriteLine("Mail : ");
            tab[2] = Console.ReadLine();
            Console.WriteLine("Status : ");
            tab[3] = Console.ReadLine();
            Console.WriteLine("ID : ");
            tab[4] = Console.ReadLine();
            Console.WriteLine("Password : ");
            tab[5] = Console.ReadLine();
            elements.Add(tab);


        }
        public int infoColumn(string word) //return the column of the information we want to manipulate
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
        public void ModifyFile() //modify an information for a particular count
        {
            List<string[]> elements = this.Infos();
            Console.WriteLine("Which information do you want to modify ? ");
            string word = Console.ReadLine().ToUpper();
            int choice = infoColumn(word);
            Console.WriteLine("For who ? ");
            string name = Console.ReadLine().ToUpper();
            int line = InformationLine(name);
            Console.WriteLine("What is the new information you want to write ? :");
            string info = Console.ReadLine();
            elements.ElementAt(line)[choice] = info;
        }
        public int InformationLine(string name) //return the line of the count we want to find
        {
            List<string[]> file = this.Infos();
            int res = -1; //if nothing is found, we return -1
            for(int i=0;i<file.Count;i++)
            {
                if(file.ElementAt(i)[0].ToUpper()==name.ToUpper())
                {
                    res = i; //we return the line of the information we are looking for
                }
            }
            return res;
        }
        public void RemoveInformation() //remove a particular count
        {
            Console.WriteLine("Which account do you want to remove ? ");
            string word = Console.ReadLine().ToUpper();
            List<string[]> elements = this.Infos();
            int choice = InformationLine(word);
            elements.RemoveAt(choice);
        }

    }
}
