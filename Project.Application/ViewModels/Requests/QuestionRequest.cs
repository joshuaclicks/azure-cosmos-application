namespace Project.Application.ViewModels.Requests
{
    public class QuestionRequest
    {
        public string Title { get; set; } = null!;
        public string QuestionTypeId {  get; set; } = null!;
        public List<string> Options { get; set; } = [];
        public short? MaximumChoiceAllowed { get; set; }
    }
}
