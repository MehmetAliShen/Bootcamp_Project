using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Business.BusinessRules
{
    public class EmployeeBusinessRules
    {
        private readonly IGenericRepository<Employee> _employeeRepository;

        public EmployeeBusinessRules(IGenericRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task CheckIfEmployeeIsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Employee email cannot be empty.");

            var existing = await _employeeRepository.FindAsync(e => e.Email == email);
            if (existing.Any())
                throw new Exception("An employee with this email already exists.");
        }
    }
}
