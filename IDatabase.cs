using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    interface IDatabase
    {
        void ModifyFile();
        void RemoveInformation(string name);
        void ShowFile();
        void AddInformation();
    }
}
