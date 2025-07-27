using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.Bootcamp;
using Business.Dtos.Responses.Bootcamp;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessRules;
using Repositories;

namespace Business.Concretes
{
    public class BootcampManager : IBootcampService
    {
        private readonly IGenericRepository<Bootcamp> _repository;
        private readonly BootcampBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public BootcampManager(IGenericRepository<Bootcamp> repository, BootcampBusinessRules businessRules, IMapper mapper)
        {
            _repository = repository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<GetBootcampResponse> CreateAsync(CreateBootcampRequest request)
        {
            await _businessRules.CheckIfBootcampCanBeCreated(request.Name, request.InstructorId, request.StartDate, request.EndDate);

            var entity = _mapper.Map<Bootcamp>(request);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<GetBootcampResponse>(entity);
        }

        public async Task UpdateAsync(int id, UpdateBootcampRequest request)
        {
            var bootcamp = await _repository.GetByIdAsync(id);
            if (bootcamp == null) throw new Exception("Bootcamp not found.");

            _mapper.Map(request, bootcamp);
            _repository.Update(bootcamp);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Bootcamp not found.");
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetBootcampResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetBootcampResponse>(entity);
        }

        public async Task<IEnumerable<GetBootcampResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetBootcampResponse>>(entities);
        }
    }
}
