using Project.DataAccess.DataObjects;
using Project.DataAccess.Interfaces;

namespace Project.DataAccess.Models
{
    public class CandidateResponse : IDatedIdentityCollection
    {
        public DateTime DateCreated { get; set; }
        public string Id { get; set; } = null!;
        public string CandidateId { get; set; } = null!;
        public ICollection<QuestionResponseDto> Responses { get; set; } = [];
    }
}
