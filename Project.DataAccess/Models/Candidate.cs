using Project.DataAccess.DataObjects;
using Project.DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DataAccess.Models
{
    public class Candidate : IDatedIdentityCollection
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;

        public CountryDto Nationality { get; set; } = null!;

        public CountryDto CountryOfResidence { get; set; } = null!;
        public string CandidateId { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string Id { get; set; } = null!;
    }
}
