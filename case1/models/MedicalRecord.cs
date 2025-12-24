namespace case1.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string DoctorRemarks { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        
        // Navigation property
        public Patient Patient { get; set; } = null!;
    }
}
