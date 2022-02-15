using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry_Conpetition
{
    class ChildPoets :Poets
    {
        public string FIOParent;
        public string Education;

        public ChildPoets()
        {
            FIO = string.Empty;
            Type = "Дети";
            Category = string.Empty; ;
            FIOParent = string.Empty;
            Education = string.Empty;
        }

        public ChildPoets(string tFIO, string tCategory, string tFIOParent, string tEducation) 
        {
            ID = GetID();
            FIO = tFIO;
            Type = "Дети";
            Category = tCategory;
            FIOParent = tFIOParent;
            Education = tEducation;
        }
        
    }

    
}
