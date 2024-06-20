using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.ViewModels.Requests
{
    public class CandidateRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public string NationalityId { get; set; } = null!;
        public string CountryOfResidenceId { get; set; } = null!;
        public string CandidateIdentityNumber { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public List<CandidateAnswerRequest> Responses { get; set; } = [];
    }
}
