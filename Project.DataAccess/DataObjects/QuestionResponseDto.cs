using Project.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DataAccess.DataObjects
{
    public class QuestionResponseDto
    {
        public string QuestionId { get; set; } = null!;

        [NotMapped]
        public ICollection<QuestionOption> SelectedOptions { get; set; } = [];
        public string AnswerText { get; set; } = null!;
    }
}
