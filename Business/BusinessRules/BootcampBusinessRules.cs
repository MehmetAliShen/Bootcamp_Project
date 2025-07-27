using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Business.BusinessRules
{
    public class BootcampBusinessRules
    {
        private readonly IGenericRepository<Bootcamp> _bootcampRepository;
        private readonly IGenericRepository<Instructor> _instructorRepository;

        public BootcampBusinessRules(
            IGenericRepository<Bootcamp> bootcampRepository,
            IGenericRepository<Instructor> instructorRepository)
        {
            _bootcampRepository = bootcampRepository;
            _instructorRepository = instructorRepository;
        }

        public async Task CheckIfBootcampCanBeCreated(string name, int instructorId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new Exception("Start date must be before end date.");

            var duplicate = await _bootcampRepository.FindAsync(b => b.Name == name);
            if (duplicate.Any())
                throw new Exception("Bootcamp with the same name already exists.");

            var instructor = await _instructorRepository.GetByIdAsync(instructorId);
            if (instructor == null)
                throw new Exception("Instructor must exist in the system.");

        }
    }
}
