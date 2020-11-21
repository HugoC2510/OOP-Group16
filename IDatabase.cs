using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    interface IDatabase
    {
        void ModifyFile();
        void RemoveInformation();
        void ShowFile();
        void AddInformation();
        void WriteInCsv();
    }
}
