using Business.Dtos.Requests.Employee;
using Business.Dtos.Responses.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IEmployeeService
    {
        Task<GetEmployeeResponse> GetByIdAsync(int id);
        Task<IEnumerable<GetEmployeeResponse>> GetAllAsync();
        Task<GetEmployeeResponse> CreateAsync(CreateEmployeeRequest request);
        Task UpdateAsync(int id, UpdateEmployeeRequest request);
        Task DeleteAsync(int id);
    }
}
