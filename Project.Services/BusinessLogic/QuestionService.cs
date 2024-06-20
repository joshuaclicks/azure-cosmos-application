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
    public class QuestionService(EmployerApplicationContext dbContext) : IQuestionService
    {
        private readonly EmployerApplicationContext _dbContext = dbContext;

        public async Task<ApiResponse> CreateQuestionType(QuestionTypeRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var questionType = await _dbContext.QuestionTypes.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower().Trim(), cancellationToken);

                if (questionType != null)
                    return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPE_ALREADY_EXIST, ErrorMessages.QUESTION_TYPE_ALREADY_EXIST);

                var id = Guid.NewGuid().ToString("N");
                await _dbContext.QuestionTypes.AddAsync(new QuestionType
                {
                    Id = id,
                    Name = request.Name.Trim(),
                    IsDateTyped = request.IsDateTyped,
                    IsMultiSelect = request.IsMultiSelect,
                    IsOptionTyped = request.IsOptionTyped,
                    IsTextTyped = request.IsTextTyped
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

        public async Task<ApiResponse> CreateQuestions(ProgramQuestionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var program = await _dbContext.Programs.FirstOrDefaultAsync(x => x.Id == request.ProgramId.Trim(), cancellationToken);

                if (program == null)
                    return ResponseHandler.FailureResponse(ErrorCodes.PROGRAM_NOT_FOUND, ErrorMessages.PROGRAM_NOT_FOUND);

                var questionTypes = await _dbContext.QuestionTypes.ToListAsync(cancellationToken);
                if (questionTypes.Count == 0)
                    return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPES_NOT_FOUND, ErrorMessages.QUESTION_TYPES_NOT_FOUND);

                List<Question> questionList = [];
                for (int index = 0; index < request.Questions.Count; index++)
                {
                    var questionType = questionTypes.FirstOrDefault(x => x.Id == request.Questions[index].QuestionTypeId.Trim());
                    if (questionType == null)
                        return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPES_INVALID, ErrorMessages.QUESTION_TYPES_INVALID);

                    var questionId = Guid.NewGuid().ToString("N");
                    Question question = new()
                    {
                        Id = questionId,
                        MaximumChoiceAllowed = request.Questions[index].MaximumChoiceAllowed,
                        ProgramId = request.ProgramId,
                        Title = request.Questions[index].Title.Trim(),
                        QuestionType = new QuestionTypeDto
                        {
                            Id = questionType.Id,
                            IsDateTyped = questionType.IsDateTyped,
                            IsMultiSelect = questionType.IsMultiSelect,
                            IsOptionTyped = questionType.IsOptionTyped,
                            IsTextTyped = questionType.IsTextTyped,
                            Name = questionType.Name,
                        }
                    };

                    if (request.Questions[index].Options.Count > 0)
                    {
                        for (int optionIndex = 0; optionIndex < request.Questions[index].Options.Count; optionIndex++)
                        {
                            var optionId = Guid.NewGuid().ToString("N");
                            question.Options.Add(new OptionDto
                            {
                                Id = optionId,
                                Option = request.Questions[index].Options[optionIndex]
                            });
                        }
                    }

                    questionList.Add(question);
                }

                await _dbContext.Questions.AddRangeAsync(questionList, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return ResponseHandler.SuccessResponse(SuccessMessages.DEFAULT_SUCCESS_MESSAGE, SuccessCodes.DEFAULT_SUCCESS_CODE);
            }
            catch
            {
                return ResponseHandler.FailureResponse(ErrorCodes.DEFAULT_EXCEPTION, ErrorMessages.SERVER_ERROR);
            }
        }

        public async Task<ApiResponse> GetQuestions(string programId, CancellationToken cancellationToken = default)
        {
            try
            {
                var questions = await _dbContext.Questions.Where(x => x.ProgramId == programId.Trim()).ToListAsync(cancellationToken);

                List<QuestionResponse> programQuestions = [];
                if (questions.Count > 0)
                {
                    var questionTypes = await _dbContext.QuestionTypes.ToListAsync(cancellationToken);
                    programQuestions = questions.Select(x => new QuestionResponse
                    {
                        Id = x.Id,
                        MaximumChoiceAllowed = x.MaximumChoiceAllowed,
                        QuestionType =  new QuestionTypeResponse
                        {
                            IsDateTyped = x.QuestionType.IsDateTyped,
                            IsMultiSelect = x.QuestionType.IsMultiSelect,
                            IsOptionTyped = x.QuestionType.IsOptionTyped,
                            IsTextTyped = x.QuestionType.IsTextTyped,
                            Name = x.QuestionType.Name,
                            Id = x.QuestionType.Id
                        },
                        Title = x.Title,
                        Options = x.Options.Count == 0 ? [] : x.Options.Select(s => new QuestionOptionResponse
                        {
                            Id = s.Id,
                            Option = s.Option
                        }).ToList()
                    }).ToList();
                }


                return ResponseHandler.SuccessResponse(SuccessMessages.DEFAULT_SUCCESS_MESSAGE, SuccessCodes.DEFAULT_SUCCESS_CODE, programQuestions);
            }
            catch
            {
                return ResponseHandler.FailureResponse(ErrorCodes.DEFAULT_EXCEPTION, ErrorMessages.SERVER_ERROR);
            }
        }

        public async Task<ApiResponse> UpdateQuestions(List<UpdateQuestionRequest> request, CancellationToken cancellationToken = default)
        {
            try
            {
                var questionTypes = await _dbContext.QuestionTypes.ToListAsync(cancellationToken);
                if (questionTypes.Count == 0)
                    return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPES_NOT_FOUND, ErrorMessages.QUESTION_TYPES_NOT_FOUND);

                var questionIds = request.Select(x => x.QuestionId).ToList();
                var selectedQuestions = await _dbContext.Questions.Where(x => questionIds.Contains(x.Id)).ToListAsync(cancellationToken);

                List<Question> questionList = [];
                for (int index = 0; index < request.Count; index++)
                {
                    var questionType = questionTypes.FirstOrDefault(x => x.Id != request[index].QuestionTypeId.Trim());
                    if (questionType == null)
                        return ResponseHandler.FailureResponse(ErrorCodes.QUESTION_TYPES_INVALID, ErrorMessages.QUESTION_TYPES_INVALID);

                    var question = selectedQuestions.FirstOrDefault(x => x.Id == request[index].QuestionId.Trim());
                    if (question == null)
                        return ResponseHandler.FailureResponse(ErrorCodes.QUESTIONS_INVALID, ErrorMessages.QUESTIONS_INVALID);

                    if (request[index].MaximumChoiceAllowed.HasValue)
                    {
                        question.MaximumChoiceAllowed = request[index].MaximumChoiceAllowed.Value;
                    }

                    if (!string.IsNullOrWhiteSpace(request[index].Title))
                    {
                        question.Title = request[index].Title;
                    }

                    if (!string.IsNullOrWhiteSpace(request[index].QuestionTypeId))
                    {
                        question.QuestionType = new QuestionTypeDto
                        {
                            Name = questionType.Name,
                            IsTextTyped = questionType.IsTextTyped,
                            IsOptionTyped = questionType.IsOptionTyped,
                            IsMultiSelect = questionType.IsMultiSelect,
                            IsDateTyped = questionType.IsDateTyped,
                            Id = questionType.Id
                        };
                    }

                    if (request[index].Options.Count > 0)
                    {
                        question.Options = [];
                        for (int optionIndex = 0; optionIndex < request[index].Options.Count; optionIndex++)
                        {
                            var optionId = Guid.NewGuid().ToString("N");
                            question.Options.Add(new OptionDto
                            {
                                Id = optionId,
                                Option = request[index].Options[optionIndex]
                            });
                        }
                    }

                    questionList.Add(question);
                }

                if (questionList.Count > 0)
                {
                    _dbContext.Questions.UpdateRange(questionList);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                return ResponseHandler.SuccessResponse(SuccessMessages.DEFAULT_SUCCESS_MESSAGE, SuccessCodes.DEFAULT_SUCCESS_CODE);
            }
            catch 
            {
                return ResponseHandler.FailureResponse(ErrorCodes.DEFAULT_EXCEPTION, ErrorMessages.SERVER_ERROR);
            }
            
        }
    }
}
