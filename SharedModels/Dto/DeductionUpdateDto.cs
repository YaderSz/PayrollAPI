using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dto
{
    public class DeductionUpdateDto
    {
        [Required]
        public int DeductionID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public double INSS { get; set; }
        [Required]
        public double IR { get; set; }
        [Required]
        public double TotalDeductions { get; set; }
    }
}
