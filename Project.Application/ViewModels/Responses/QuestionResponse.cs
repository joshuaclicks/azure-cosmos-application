namespace Project.Application.ViewModels.Responses
{
    public class QuestionResponse
    {
        public string Id { get; set; } = null!;
        public QuestionTypeResponse QuestionType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public IList<QuestionOptionResponse> Options { get; set; } = [];
        public short? MaximumChoiceAllowed { get; set; }
    }
}
