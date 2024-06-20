using Microsoft.EntityFrameworkCore;
using Project.Application.Constants;
using Project.Application.Contracts;
using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;
using Project.Application.ViewModels.Responses;
using Project.DataAccess.Contexts;
using Project.DataAccess.Models;

namespace Project.Services.BusinessLogic
{
    public class ProgramService(EmployerApplicationContext dbContext) : IProgramService
    {
        private readonly EmployerApplicationContext _dbContext = dbContext;

        public async Task<ApiResponse> CreateProgram(ProgramRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var program = await _dbContext.Programs.FirstOrDefaultAsync(x => x.Title.ToLower() == request.Title.ToLower().Trim(), cancellationToken);

                if (program != null)
                    return ResponseHandler.FailureResponse(ErrorCodes.PROGRAM_TYPE_ALREADY_EXIST, ErrorMessages.PROGRAM_TYPE_ALREADY_EXIST);

                var id = Guid.NewGuid().ToString("N");
                await _dbContext.Programs.AddAsync(new Program
                {
                    Id = id,
                    Title = request.Title,
                    Description = request.Description,
                    DateCreated = DateTime.UtcNow
                }, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                GenericIdentityResponse response = new()
                {
                    Id = id
                };

                return ResponseHandler.SuccessResponse(SuccessMessages.DEFAULT_SUCCESS_MESSAGE, SuccessCodes.DEFAULT_SUCCESS_CODE, response);
            }
            catch
            {
                return ResponseHandler.FailureResponse(ErrorCodes.DEFAULT_EXCEPTION, ErrorMessages.SERVER_ERROR);
            }
        }

    }
}
