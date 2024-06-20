using Microsoft.AspNetCore.Mvc;
using Project.Application.Contracts;
using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;
using Project.Application.ViewModels.Responses;
using Project.DataAccess.Models;
using Project.Services.BusinessLogic;

namespace Project.APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IQuestionService questionService) : BaseController
    {
        private readonly IQuestionService _questionService = questionService;

        /// <summary>
        /// Endpoint to create Question Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("type")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<GenericIdentityResponse>))]
        public async Task<IActionResult> CreateQuestionType([FromBody] QuestionTypeRequest request, CancellationToken cancellationToken = default) => Response(await _questionService.CreateQuestionType(request, cancellationToken));

        /// <summary>
        /// Endpoint to create Questions
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<>))]
        public async Task<IActionResult> CreateQuestions([FromBody]ProgramQuestionRequest request, CancellationToken cancellationToken = default) => Response(await _questionService.CreateQuestions(request, cancellationToken));

        /// <summary>
        /// Endpoint to get Questions
        /// </summary>
        /// <param name="programId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{programId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<List<QuestionResponse>>))]
        public async Task<IActionResult> GetQuestions(string programId, CancellationToken cancellationToken = default) => Response(await _questionService.GetQuestions(programId, cancellationToken));

        /// <summary>
        /// Endpoint to Update Questions
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<>))]
        public async Task<IActionResult> UpdateQuestions([FromBody] List<UpdateQuestionRequest> request, CancellationToken cancellationToken = default) => Response(await _questionService.UpdateQuestions(request, cancellationToken));
    }
}
