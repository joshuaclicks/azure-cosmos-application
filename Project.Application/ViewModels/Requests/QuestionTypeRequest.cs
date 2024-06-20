namespace Project.Application.ViewModels.Requests
{
    public class QuestionTypeRequest
    {
        public required string Name { get; set; }
        public bool IsOptionTyped { get; set; }
        public bool IsDateTyped { get; set; }
        public bool IsTextTyped { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}
