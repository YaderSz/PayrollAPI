using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Deduction
    {
        public int DeductionID { get; set; }
        public int EmployeeID { get; set; }
        public double Salario { get; set; }

        public double INSS {  get; set; }
        public double IR {  get; set; }
        public double TotalDeductions { get; set; }
        public Employee? Employees { get; set; }
    }
}
