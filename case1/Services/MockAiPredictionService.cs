using case1.DTOs;

namespace case1.Services
{
    public class MockAiPredictionService : IAiPredictionService
    {
        private readonly Random _random = new();
        private readonly string[] _predictions = { "Low Risk", "Medium Risk", "High Risk" };

        public AiPredictionResponse GetPrediction(int patientId)
        {
            // Belirleyici ama deðiþken sahte veriler döndür
            var index = patientId % 3;
            return new AiPredictionResponse
            {
                Prediction = _predictions[index],
                Confidence = 0.75 + (_random.NextDouble() * 0.2) // 0.75-0.95
            };
        }
    }
}
