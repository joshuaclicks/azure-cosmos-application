using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;

namespace Project.Application.Contracts
{
    public interface ICandidateService
    {
        Task<ApiResponse> SubmitApplication(CandidateRequest request, CancellationToken cancellationToken = default);
    }
}