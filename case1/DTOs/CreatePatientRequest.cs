namespace case1.DTOs
{
    public class CreatePatientRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
