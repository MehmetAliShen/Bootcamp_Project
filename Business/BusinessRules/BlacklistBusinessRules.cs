using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Business.BusinessRules
{
    public class BlacklistBusinessRules
    {
        private readonly IGenericRepository<Blacklist> _blacklistRepository;

        public BlacklistBusinessRules(IGenericRepository<Blacklist> blacklistRepository)
        {
            _blacklistRepository = blacklistRepository;
        }

        public async Task CheckIfCanBeBlacklisted(int applicantId, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new Exception("Blacklist reason cannot be empty.");

            var existingBlacklists = await _blacklistRepository.FindAsync(b => b.ApplicantId == applicantId);
            if (existingBlacklists.Any())
                throw new Exception("This applicant is already blacklisted.");
        }
    }
}
