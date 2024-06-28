using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? Cedula { get; set; }
        public int INSS { get; set; }
        public int RUC { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? LastName2 { get; set; }

        public DateOnly Nacimiento { get; set; }
        public string? Sexo { get; set; }
        public string? EstadoCivil { get; set; }

        public string? Direccion { get; set; }
        public int Telefono { get; set; }
        public int Celular { get; set; }

        public DateOnly Contratacion { get; set; }
        public DateOnly FinContratacion { get; set; }
        public double Salario { get; set; }
        public bool Activo { get; set; }
        
        public double SalarioNeto {  get; set; }
        public int PayrollId { get; set; }
        public int DeductionID { get; set; }
        public int IncomeId { get; set; }

        public Payroll? Payroll { get; set; }
        public ICollection<Income>? Incomes { get; set; }
        public ICollection<Deduction>? Deductions { get; set;}

    }
}
