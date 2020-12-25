using System.ServiceProcess;
using System.Threading;
using System.IO;
using Northwind.ConfigurationManager.Provider;
using Northwind.DataManagerService.DataManagerSettings;
using Northwind.Models;
using Northwind.ServiceLayer.DataTransfer;
using Northwind.ServiceLayer.XMLGenrator;
using System;

namespace Northwind.DataManagerService
{
    public partial class DataManagerService : ServiceBase
    {
        private Settings configSettings;

        public DataManagerService()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            try
            {
                var settingsManager = new SettingsManager(AppDomain.CurrentDomain.BaseDirectory);
                configSettings = settingsManager.GetSettings<Settings>();
                //configSettings = await settingsManager.GetSettingsAsync<Settings>();

                /*XMLGenerator<Employee> generator = new XMLGenerator<Employee>(new Collection(configSettings.ConnectionString).GetEmployees());
                FileTransfer fileTransfer = new FileTransfer();*/
                XMLGenerator<Employee> generator = new XMLGenerator<Employee>(await new Collection(configSettings.ConnectionString).GetEmployeesAsync());
                FileTransfer fileTransfer = new FileTransfer();
                using (FileStream file = new FileStream(await generator.GenerateXMLAsync(), FileMode.Open))
                {
                    await fileTransfer.CopyAsync(file, configSettings.FIleWatcherSourceFolder);
                }
                using (FileStream file = new FileStream(await generator.GenerateXSDAsync(), FileMode.Open))
                {
                    await fileTransfer.CopyAsync(file, configSettings.FIleWatcherSourceFolder);
                }
            }
            catch(Exception ex)
            {
                await new Collection(configSettings == null ? "Data Source=DESKTOP-USMASL0;Initial Catalog=AdventureWorks2019;Integrated Security=True;" : configSettings.ConnectionString).WriteErrorAsync(ex);
            }            
        }

        protected override void OnStop()
        {
        }

        public void OnDebug()
        {
            OnStart(null);
        }
    }
}
