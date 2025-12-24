namespace case1.DTOs
{
    public class PatientDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public List<MedicalRecordDto> Records { get; set; } = new();
    }

    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DoctorRemarks { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
    }
}
