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
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            var settingsManager = new SettingsManager(AppDomain.CurrentDomain.BaseDirectory);
            configSettings = settingsManager.GetSettings<Settings>();

            XMLGenerator<Employee> generator = new XMLGenerator<Employee>(new Collection(configSettings.ConnectionString).GetEmployees());
            FileTransfer fileTransfer = new FileTransfer();
            using (FileStream file = new FileStream(generator.GenerateXML(), FileMode.Open))
            {
                fileTransfer.Copy(file, configSettings.FIleWatcherSourceFolder);
            }
            using (FileStream file = new FileStream(generator.GenerateXSD(), FileMode.Open))
            {
                fileTransfer.Copy(file, configSettings.FIleWatcherSourceFolder);
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
