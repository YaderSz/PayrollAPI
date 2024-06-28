using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Payroll
    {
        public int PayrollId {  get; set; }
        public DateOnly FechaPeriodo { get; set; }
        
     
        public int EmployeeID {  get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
