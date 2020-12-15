using System.ServiceProcess;

namespace Northwind.DataManagerService
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
