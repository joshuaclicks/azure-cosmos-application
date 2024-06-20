using Microsoft.EntityFrameworkCore;
using Project.Application.Constants;
using Project.Application.Contracts;
using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;
using Project.Application.ViewModels.Responses;
using Project.DataAccess.Contexts;
using Project.DataAccess.DataObjects;
using Project.DataAccess.Models;

namespace Project.Services.BusinessLogic
{
    public class CandidateService(EmployerApplicationContext dbContext) : ICandidateService
    {
        private readonly EmployerApplicationContext _dbContext = dbContext;

        public async Task<ApiResponse> SubmitApplication(CandidateRequest request, CancellationToken cancellationToken = default)
        {
            var selectedCandidate = await _dbContext.Candidates.FirstOrDefaultAsync(x => x.EmailAddress.ToLower() == request.EmailAddress.ToLower().Trim(), cancellationToken);

            if (selectedCandidate != null)
                return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPE_ALREADY_EXIST, ErrorMessages.QUESTION_TYPE_ALREADY_EXIST);

            if (request.Responses.Count == 0)
                return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPE_ALREADY_EXIST, ErrorMessages.QUESTION_TYPE_ALREADY_EXIST);

            var countries = await _dbContext.Countries.Where(x => x.Id == request.NationalityId || x.Id == request.CountryOfResidenceId).ToListAsync(cancellationToken);

            var nationality = countries.FirstOrDefault(x => x.Id == request.NationalityId);
            var residentialCountry = countries.FirstOrDefault(x => x.Id == request.CountryOfResidenceId);
            if (residentialCountry == null)
                return ResponseHandler.FailureResponse(ErrorCodes.RESIDENTIAL_COUNTRY_INVALID, ErrorMessages.RESIDENTIAL_COUNTRY_INVALID);

            if (nationality == null)
                return ResponseHandler.FailureResponse(ErrorCodes.NATIONALITY_INVALID, ErrorMessages.NATIONALITY_INVALID);

            var candidateId = Guid.NewGuid().ToString("N");
            Candidate candidate = new()
            {
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                CandidateId = request.CandidateIdentityNumber,
                DateCreated = DateTime.UtcNow,
                DateOfBirth = request.DateOfBirth,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                PhoneCode = request.PhoneCode,
                CountryOfResidence = new DataAccess.DataObjects.CountryDto
                {
                    DialingCode = residentialCountry.DialingCode,
                    Id = residentialCountry.Id,
                    Name = residentialCountry.Name,
                    ShortCode = residentialCountry.ShortCode
                },
                Nationality = new DataAccess.DataObjects.CountryDto
                {
                    DialingCode = nationality.DialingCode,
                    Id = nationality.Id,
                    Name = nationality.Name,
                    ShortCode = nationality.ShortCode
                },
                Id = candidateId
            };

            var responseId = Guid.NewGuid().ToString("N");
            CandidateResponse candidateResponse = new()
            {
                CandidateId = candidateId,
                DateCreated = DateTime.UtcNow,
                Id = responseId
            };

            var questionIds = request.Responses.Select(x => x.QuestionId).ToList();
            var questions = await _dbContext.Questions.Where(x => questionIds.Contains(x.Id)).ToListAsync(cancellationToken);

            List<QuestionResponseDto> candidateResponses = new();
            for (int index = 0; index < request.Responses.Count; index++)
            {
                var question = questions.FirstOrDefault(x => x.Id == request.Responses[index].QuestionId);
                if (question == null)
                    return ResponseHandler.FailureResponse(ErrorCodes.RESPONSE_QUESTIONS_INVALID, ErrorMessages.RESPONSE_QUESTIONS_INVALID);

                QuestionResponseDto questionResponseDto = new()
                {
                    QuestionId = request.Responses[index].QuestionId,
                    AnswerText = request.Responses[index].ResponseText
                };

                if (request.Responses[index].SelectedOptionIds.Count > 0)
                {
                    for (int optionIndex = 0; optionIndex < request.Responses[index].SelectedOptionIds.Count; optionIndex++)
                    {
                        questionResponseDto.SelectedOptions.Add(new QuestionOption
                        {
                            Id = request.Responses[index].SelectedOptionIds[optionIndex],
                            Option = question.Options.FirstOrDefault(x => x.Id == request.Responses[index].SelectedOptionIds[optionIndex]).Option
                        });
                    }

                }

                candidateResponses.Add(questionResponseDto);
            }

            candidateResponse.Responses = candidateResponses;

            await _dbContext.Candidates.AddAsync(candidate, cancellationToken);
            await _dbContext.CandidateResponses.AddAsync(candidateResponse, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            GenericIdentityResponse response = new()
            {
                Id = candidateId
            };

            return ResponseHandler.SuccessResponse(SuccessMessages.DEFAULT_SUCCESS_MESSAGE, SuccessCodes.DEFAULT_SUCCESS_CODE, response);
        }
    }
}
