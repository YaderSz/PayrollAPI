using PayrolAPI.Repository;
using PayrolAPI.Repository.IRepository;
using SharedModels.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        public IRepository<EmployesDto> Employees { get; set; }
        public IRepository<IncomeDto> Incomes { get; set; }
        public IRepository<DeductionDto> Deductions { get; set; }
        public IRepository<PayrollDto> Payrolls { get; set; }
        public IUserRepository LoginUsers { get; }



        public ApiClient()
        {
            string apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            _httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
            Employees = new Repository<EmployesDto>(_httpClient, "Employee");
            Incomes = new Repository<IncomeDto>(_httpClient, "Income");
            Deductions = new Repository<DeductionDto>(_httpClient, "Deduction");
            Payrolls = new Repository<PayrollDto>(_httpClient, "Payroll");
            LoginUsers = new UserRepository(_httpClient, "Auth/Login");


        }

        public void SetBasicAuthCredentials(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        internal void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
