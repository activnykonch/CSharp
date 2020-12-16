using System;

namespace Northwind.Models
{
    public class Error : Exception
    {
        public string ErrorMessage { get; private set; }
        public string Instance { get; private set; }
        public DateTime Time { get; private set; }

        public Error(string message, string instance, DateTime time)
        {
            this.ErrorMessage = message;
            this.Instance = instance;
            this.Time = time;
        }
    }
}
