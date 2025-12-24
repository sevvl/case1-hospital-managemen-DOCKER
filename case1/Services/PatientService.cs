using case1.Data;
using case1.DTOs;
using case1.Models;
using Microsoft.EntityFrameworkCore;

namespace case1.Services
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            return await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Surname = p.Surname,
                    BirthDate = p.BirthDate
                })
                .ToListAsync();
        }

        public async Task<PatientDetailDto?> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Records)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return null;

            return new PatientDetailDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Surname = patient.Surname,
                BirthDate = patient.BirthDate,
                Records = patient.Records.Select(r => new MedicalRecordDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    DoctorRemarks = r.DoctorRemarks,
                    Diagnosis = r.Diagnosis
                }).ToList()
            };
        }

        public async Task<PatientDto> CreateAsync(CreatePatientRequest request)
        {
            var patient = new Patient
            {
                Name = request.Name,
                Surname = request.Surname,
                BirthDate = request.BirthDate
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Surname = patient.Surname,
                BirthDate = patient.BirthDate
            };
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
