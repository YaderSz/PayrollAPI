using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dto
{
    public class PayrollUpdateDto
    {
        [Required]
        public int PayrollId { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateOnly FechaPeriodo { get; set; }
       

    }
}
