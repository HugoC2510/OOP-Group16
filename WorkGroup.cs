using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public class WorkGroup
    {
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
        //public void Equal(WorkGroup group)
        //{
        //    foreach(Student stud in group)
        //    {
        //        if(Student)
        //    }
        //}
        

    }
}
