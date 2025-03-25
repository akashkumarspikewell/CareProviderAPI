using System.ComponentModel.DataAnnotations;

namespace CareProviderAPI.Data.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public List<CareProvider> CareProviders { get; set; } = new();
    }
}
