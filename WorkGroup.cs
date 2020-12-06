using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class WorkGroup
    {
        //made by: 
        //23168 Hugo Camps
        //23175 Albert De Watrigant
        //23196 Aurelien Delicourt
        //23172 Jean-Marc Hanna
        //22842 Julien Msika
        //22830 Lorenzo Mendes

        public List<Student> members;
        public string name;
        public Teacher professor;
        public WorkGroup(string name)
        {
            this.name = name;
            members = new List<Student>();
            this.professor = null;
        }
        public WorkGroup(List<Student> _members, string _name)
        {
            this.members = _members;
            this.name = _name;
        }
        public WorkGroup(List<Student> _members, string _name, Teacher _professor)
        {
            this.members = _members;
            this.name = _name;
            this.professor = _professor;
        }

        public bool StudentInWorkGroup(Student student)
        {
            foreach(Student stud in this.members)
            {
                if (stud.EqualPerson(student) == true)
                {
                    return true;
                }
            }
            return false;
        }      
    }
}
