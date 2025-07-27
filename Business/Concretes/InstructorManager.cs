using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.Instructor;
using Business.Dtos.Responses.Instructor;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.BusinessRules;
using Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes
{
    public class InstructorManager : IInstructorService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Instructor> _instructorRepository;
        private readonly InstructorBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public InstructorManager(
            IGenericRepository<User> userRepository,
            IGenericRepository<Instructor> instructorRepository,
            InstructorBusinessRules businessRules,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _instructorRepository = instructorRepository;
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

        public async Task<GetInstructorResponse> CreateAsync(CreateInstructorRequest request)
        {
            
            await _businessRules.CheckIfInstructorIsValid(request.Email);

            
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var entity = _mapper.Map<Instructor>(request);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            await _instructorRepository.AddAsync(entity);
            try
            {
                await _instructorRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

            return _mapper.Map<GetInstructorResponse>(entity);
        }

        public async Task UpdateAsync(int id, UpdateInstructorRequest request)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null) throw new Exception("Instructor not found.");

            
            var context = (_instructorRepository as GenericRepository<Instructor>)?.Context;
            if (context != null)
            {
                
                var local = context.Set<Instructor>().Local.FirstOrDefault(x => x.Id == id);
                if (local != null)
                {
                    context.Entry(local).State = EntityState.Detached;
                }
            }

            _mapper.Map(request, instructor);

            _instructorRepository.Update(instructor);
            await _instructorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _instructorRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Instructor not found.");

            _instructorRepository.Delete(entity);
            await _instructorRepository.SaveChangesAsync();
        }

        public async Task<GetInstructorResponse> GetByIdAsync(int id)
        {
            var entity = await _instructorRepository.GetByIdAsync(id);
            return _mapper.Map<GetInstructorResponse>(entity);
        }

        public async Task<IEnumerable<GetInstructorResponse>> GetAllAsync()
        {
            var entities = await _instructorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetInstructorResponse>>(entities);
        }
    }
}
