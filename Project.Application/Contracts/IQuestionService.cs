using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;

namespace Project.Application.Contracts
{
    public interface IQuestionService
    {
        Task<ApiResponse> CreateQuestions(ProgramQuestionRequest request, CancellationToken cancellationToken = default);
        Task<ApiResponse> CreateQuestionType(QuestionTypeRequest request, CancellationToken cancellationToken = default);
        Task<ApiResponse> GetQuestions(string programId, CancellationToken cancellationToken = default);
        Task<ApiResponse> UpdateQuestions(List<UpdateQuestionRequest> request, CancellationToken cancellationToken = default);
    }
}