using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;

namespace Project.Application.Contracts
{
    public interface IProgramService
    {
        Task<ApiResponse> CreateProgram(ProgramRequest request, CancellationToken cancellationToken = default);
    }
}
