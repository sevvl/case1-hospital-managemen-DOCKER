using case1.DTOs;

namespace case1.Services
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllAsync();
        Task<PatientDetailDto?> GetByIdAsync(int id);
        Task<PatientDto> CreateAsync(CreatePatientRequest request);
        Task DeleteAsync(int id);
    }
}
