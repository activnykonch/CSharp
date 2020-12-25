using System.ServiceProcess;
using System.Threading;

namespace Northwind.DataManagerService
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            DataManagerService s = new DataManagerService();
            s.OnDebug();
            Thread.Sleep(Timeout.Infinite);
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
