using Business.Dtos.Requests.Applicant;
using Business.Dtos.Requests.Login;
using Business.Dtos.Responses.Applicant;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthService
    {
        Task<Applicant> RegisterAsync(CreateApplicantRequest request, string password);
        Task<Applicant> LoginAsync(LoginRequest request);
        string CreateAccessToken(Applicant applicant);
    }
}
