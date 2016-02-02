using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace c_Shap_calculateMemory
{
    public class Program
    {
        static void Main(string[] args)
        {

            /******** call perfomance task manager ******/ 
           // PerformanceInfo.Display();
            PerformanceInfo per = new PerformanceInfo(); 
            per.Display();

            Console.ReadKey();
            /****** end call performance task manage ****/
        }
    }

}
