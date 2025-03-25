using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareProviderAPI.Data.Models
{
    public class CareProvider
    {
        [Key]
        public int ProviderId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(500)]
        public string Bio { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public DateTime? LeaveDate { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        public List<Achievement> Achievements { get; set; } = new();
        public List<Experience> Experiences { get; set; } = new();
    }
}
