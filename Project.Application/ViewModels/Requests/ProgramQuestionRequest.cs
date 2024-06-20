namespace Project.Application.ViewModels.Requests
{
    public class ProgramQuestionRequest
    {
        public string ProgramId { get; set; } = null!;
        public List<QuestionRequest> Questions = [];
    }
}
