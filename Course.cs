using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class Course
    {
        public List<int> marks;
        public List<WorkGroup> allGroups;
        public List<Teacher> teachers;
        public string name;

        public Course(string _name,List<int> _marks, List<WorkGroup> _allGroups, List<Teacher> _teachers)
        {
            this.marks = _marks;
            this.allGroups = _allGroups;
            this.teachers = _teachers;
            this.name = _name;
        }
    }
}
