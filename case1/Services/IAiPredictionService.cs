using case1.DTOs;

namespace case1.Services
{
    public interface IAiPredictionService
    {
        AiPredictionResponse GetPrediction(int patientId);
    }
}
