using case1.DTOs;
using case1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace case1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PredictionController : ControllerBase
    {
        private readonly IAiPredictionService _aiService;

        public PredictionController(IAiPredictionService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet("{patientId}")]
        public ActionResult<AiPredictionResponse> GetPrediction(int patientId)
        {
            var prediction = _aiService.GetPrediction(patientId);
            return Ok(prediction);
        }
    }
}
