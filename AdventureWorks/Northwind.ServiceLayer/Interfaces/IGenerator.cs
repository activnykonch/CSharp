using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.ServiceLayer.Interfaces
{
    interface IGenerator
    {
        string GenerateXML();
        string GenerateXSD();
    }
}
