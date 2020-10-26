using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public class WorkGroup
    {
        public List<Student> members;
        public string name;
        public WorkGroup(List<Student> _members, string _name)
        {
            this.members = _members;
            this.name = _name;
        }
    }
}
