using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    class Program
    {
        



        static void Main(string[] args) //contains verifie un type reference  il faut trouver une autre soltuion
        {
            Student a = new Student("hugo", "camps", 20, 'M', "dfsdf", "54545454", "465656", "hugo");
            Student b = new Student("hugo", "camps", 20, 'M', "dfsdf", "54545454", "45656","hugo");
            Student c = new Student("hugo", "camps", 20, 'M', "dfsdf", "54545454", "45656", "hugo"); 
            
            List<Student> list = new List<Student>();
            list.Add(new Student("hugo", "camps", 20, 'M', "dfsdf", "54545454", "465656", "hugo"));
            list = new List<Student>();
            //list.Add(a);
            foreach(Student stud in list)
            {
                Console.WriteLine(stud.name);
            }
            
            Console.Read();
        }
    }
}
