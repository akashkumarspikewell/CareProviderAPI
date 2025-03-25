namespace CareProviderAPI.Data.DTOs
{
    public class ProviderDto
    {
        public int ProviderID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Bio { get; set; }
        public List<string> Achievements { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalExperienceYears { get; set; }
    }
}
