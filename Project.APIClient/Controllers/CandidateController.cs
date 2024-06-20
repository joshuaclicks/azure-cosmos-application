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
    public class CandidateController(ICandidateService candidateService) : BaseController
    {
        private readonly ICandidateService _candidateService = candidateService;

        /// <summary>
        /// Endpoint to submit application
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<GenericIdentityResponse>))]
        public async Task<IActionResult> SubmitApplication([FromBody] CandidateRequest request, CancellationToken cancellationToken = default) => Response(await _candidateService.SubmitApplication(request, cancellationToken));
    }
}
