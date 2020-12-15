using System;

namespace Northwind.Models
{
    public class Employee: Person
    {
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public bool SalariedFlag { get; set; }
        public decimal Rate { get; set; }
    }
}