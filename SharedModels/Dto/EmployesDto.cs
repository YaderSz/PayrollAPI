using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dto
{
    public class EmployesDto
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string? Cedula { get; set; }
       
        [Required]
        public int RUC { get; set; }
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]

        public string? SecondName { get; set; }
        [Required]
        [StringLength(50)]

        public string? LastName { get; set; }
        [StringLength(50)]

        public string? LastName2 { get; set; }
        [Required]
        public DateOnly Nacimiento { get; set; }

        public string? Sexo { get; set; }
        [Required]
        public string? EstadoCivil { get; set; }
        [Required]
        [StringLength(100)]

        public string? Direccion { get; set; }
        public int Telefono { get; set; }
        public int Celular { get; set; }
        [Required]
        public DateOnly Contratacion { get; set; }
        [Required]
        public DateOnly FinContratacion { get; set; }
       
        [Required]
        public bool Activo { get; set; }

        [Required]
        public double SalarioNeto { get; set; }
        
        public int PayrollId { get; set; }
      
       
    }
}
