namespace CareProviderAPI.Data.DTOs
{
    public class AddCareProviderDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public int DepartmentID { get; set; }
    }
}
