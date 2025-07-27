using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Business.BusinessRules
{
    public class InstructorBusinessRules
    {
        private readonly IGenericRepository<Instructor> _instructorRepository;

        public InstructorBusinessRules(IGenericRepository<Instructor> instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task CheckIfInstructorIsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Instructor email cannot be empty.");

            var existing = await _instructorRepository.FindAsync(i => i.Email == email);
            if (existing.Any())
                throw new Exception("An instructor with this email already exists.");
        }
    }
}
