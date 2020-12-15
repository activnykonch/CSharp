/*using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Northwind.DataManagerService;

namespace AdventureWorks
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            DataManagerService s = new DataManagerService();
            s.OnDebug();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DataManagerService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
*/