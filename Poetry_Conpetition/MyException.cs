using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry_Conpetition
{
    class MyException : Exception
    {
        public MyException(string Messege) : base(Messege) { }
    }
    class MyException2 : Exception
    {
        public MyException2(string Message) : base(Message) { }
    }

}
