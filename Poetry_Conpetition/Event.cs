using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry_Conpetition
{
    class Event
    {
        public string Prov(string title) 
        {
            int k = 0;
            string res =string.Empty;
            for (int i = 0; i < Poets.MyPoets.Count; i++) 
            {
                for(int j = 0; j<Poets.MyPoets[i].Works.Count; j++)
                {
                    if (Poets.MyPoets[i].Works[j].Contains(title))
                    {
                        k++;
                        if (k > 1) 
                        {
                            res += Poets.MyPoets[i].FIO + ", ";
                        }
                        
                    }
                }
            }
            return res;
        }

        public string SearchParent(string FIOP, string FIOC) 
        {
            string res = string.Empty;
            for(int i = 0; i < Poets.MyPoets.Count; i++)
            {
                if(Poets.MyPoets[i].FIO == FIOP)
                {
                    res = $"Возможно, родитель {FIOC} принимал уже участние в конкурсе поэтов";
                }
            }
            return res;
        }

        public Event()
        {
            Form1.EquelsEventShow += new Form1.EquelsEventHandlerShow(Prov);
            Form1.EquelsEventSearch += new Form1.EquelsEventHandlerSearch(SearchParent);

        }
    }
}
