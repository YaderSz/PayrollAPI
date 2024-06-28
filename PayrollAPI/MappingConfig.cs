using AutoMapper;
using SharedModels;
using SharedModels.Dto;
namespace PayrolAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployesDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<Income, IncomeCreateDto>().ReverseMap();
            CreateMap<Income, IncomeUpdateDto>().ReverseMap();
            CreateMap<Deduction, DeductionDto>().ReverseMap();
            CreateMap<Deduction, DeductionCreateDto>().ReverseMap();
            CreateMap<Deduction, DeductionUpdateDto>().ReverseMap();
            CreateMap<Payroll,  PayrollDto>().ReverseMap();
            CreateMap<Payroll,  PayrollCreateDto>().ReverseMap();
            CreateMap<Payroll,  PayrollUpdateDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();

        }
    }
}
