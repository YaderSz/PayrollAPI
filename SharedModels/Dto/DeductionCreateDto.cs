using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dto
{
    public class DeductionCreateDto
    {
        [Required]
        public int EmployeeID { get; set; }
        public double Salario { get; set; }
        [Required]
        public double TotalDeductions { get; set; }

    }
}
