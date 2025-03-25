using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareProviderAPI.Data.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceID { get; set; }
        [Required]
        [StringLength(100)]
        public string Position { get; set; }
        [Required]
        [StringLength(100)]
        public string Organization { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        [ForeignKey("Provider")]
        public int ProviderID { get; set; }
        public CareProvider Provider { get; set; }
    }
}
