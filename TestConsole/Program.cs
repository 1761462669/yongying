using Business;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            _GetCallTarget gtc=new _GetCallTarget();
            List<GetCallTarget> timeConfig = new List<GetCallTarget>();
            gtc.GetCall(out timeConfig);
            foreach (GetCallTarget item in timeConfig)
            {
                DateTime time = Convert.ToDateTime(item.time);
                Console.WriteLine(time.Hour);
                Console.WriteLine(time.Minute);
                Console.WriteLine(time.Second);
                Console.WriteLine("下一个");
                Console.ReadLine();
            }
        }
    }
}
