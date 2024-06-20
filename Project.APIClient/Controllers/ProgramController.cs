using Microsoft.AspNetCore.Mvc;
using Project.Application.Contracts;
using Project.Application.Formatters;
using Project.Application.ViewModels.Requests;
using Project.Application.ViewModels.Responses;
using Project.Services.BusinessLogic;

namespace Project.APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController(IProgramService programService) : BaseController
    {
        private readonly IProgramService _programService = programService;

        /// <summary>
        /// Endpoint to create Program
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<GenericIdentityResponse>))]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramRequest request, CancellationToken cancellationToken = default) => Response(await _programService.CreateProgram(request, cancellationToken));
    }
}
