using Project.DataAccess.DataObjects;
using Project.DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DataAccess.Models
{
    public class Question : IIdentityCollection
    {
        public string Id { get; set; } = null!;
        public string ProgramId { get; set; } = null!;
        public QuestionTypeDto QuestionType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public ICollection<OptionDto> Options { get; set; } = [];
        public short? MaximumChoiceAllowed { get; set; }
    }
}
