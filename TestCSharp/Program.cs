using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Program
    {
        static List<Action> actions = new List<Action>();
        static void Main(string[] args)
        {
            HashSet<int> list=new HashSet<int>();
        }

        static public void Show1()
        {
            Console.WriteLine("1");
        }

        static public void Show2()
        {
            Console.WriteLine("2");
        }
        static public void Show3()
        {
            Console.WriteLine("3");
        }

        public enum Test
        {
            A=1,B=2,C=4,D=8,E=16
        }
    }
}
