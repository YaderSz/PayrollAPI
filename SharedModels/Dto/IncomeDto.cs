using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dto
{
    public class IncomeDto
    {
        [Required] 
        public int IncomeId { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public double SalarioOrdinario { get; set; }
        [Required]
        public double RiesgoLaboral { get; set; }
        [Required]
        public int AñosAntiguedad { get; set; }
        [Required]
        public double Antiguedad { get; set; }
    
        [Required]
        public double Nocturnidad { get; set; }
        [Required]
        public int CntdHorasExtras { get; set; }
        //Salario que incluye horas extras 
        [Required]
        public double SalarioExtraOrdinario { get; set; }
        [Required]
        public double TotalIncomes { get; set; }




    }
}
