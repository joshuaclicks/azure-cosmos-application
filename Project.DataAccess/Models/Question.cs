using Project.DataAccess.Interfaces;

namespace Project.DataAccess.Models
{
    public class Question : IIdentityCollection
    {
        public string Id { get; set; } = null!;
        public string ProgramId { get; set; } = null!;
        public QuestionType QuestionType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public ICollection<QuestionOption> Options { get; set; } = [];
        public short? MaximumChoiceAllowed { get; set; }
    }
}
