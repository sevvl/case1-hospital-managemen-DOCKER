using Xunit;
using case1.Controllers;
using case1.Services;
using case1.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace case1.Tests
{
    public class PredictionControllerTests
    {
        [Fact]
        public void GetPrediction_Returns_OkResult()
        {
            // Arrange
            var fakeService = new FakeAiPredictionService();
            var controller = new PredictionController(fakeService);

            // Act
            ActionResult<AiPredictionResponse> result = controller.GetPrediction(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<AiPredictionResponse>(okResult.Value);
        }
    }

    public class FakeAiPredictionService : IAiPredictionService
    {
        public AiPredictionResponse GetPrediction(int patientId)
        {
            return new AiPredictionResponse();
        }
    }
}
