using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareProviderAPI.Data.Models
{
    public class Achievement
    {
        [Key]
        public int AchievementID { get; set; }
        [Required]
        [StringLength(500)]
        public string AchievementText { get; set; }
        public DateTime DateAwarded { get; set; }
        [ForeignKey("Provider")]
        public int ProviderID { get; set; }
        public CareProvider Provider { get; set; }
    }
}
