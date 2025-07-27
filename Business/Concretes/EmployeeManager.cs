using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.Employee;
using Business.Dtos.Responses.Employee;
using Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.BusinessRules;
using Repositories;

namespace Business.Concretes
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly EmployeeBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public EmployeeManager(IGenericRepository<Employee> repository, EmployeeBusinessRules businessRules, IMapper mapper)
        {
            _repository = repository;
            _businessRules = businessRules;
            _mapper = mapper;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<GetEmployeeResponse> CreateAsync(CreateEmployeeRequest request)
        {
            await _businessRules.CheckIfEmployeeIsValid(request.Email);

            var entity = _mapper.Map<Employee>(request);

            // Password hash ve salt oluştur
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            await _repository.AddAsync(entity);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

            return _mapper.Map<GetEmployeeResponse>(entity);
        }


        public async Task UpdateAsync(int id, UpdateEmployeeRequest request)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) throw new Exception("Employee not found.");

            _mapper.Map(request, employee);

            try
            {
                _repository.Update(employee);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Employee not found.");

            try
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<GetEmployeeResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Employee not found.");
            return _mapper.Map<GetEmployeeResponse>(entity);
        }

        public async Task<IEnumerable<GetEmployeeResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetEmployeeResponse>>(entities);
        }
    }
}