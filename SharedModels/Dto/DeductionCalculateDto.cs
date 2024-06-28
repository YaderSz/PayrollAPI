using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dto
{
    public class DeductionCalculateDto
    {
        public int EmployeeID { get; set; }

        public double INSS { get; set; }
        public double IR { get; set; }

    }
}
