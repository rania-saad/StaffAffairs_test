using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? JobName { get; set; }
        public int? JobPriority { get; set; }

        [ForeignKey("JobType")]
        public int JobTypeId { get; set; }
        public virtual JobType? JobType { get; set; }

        [ForeignKey("EntedabType")]
        public int EntedabTypeId { get; set; }
        public virtual EntedabType? EntedabType { get; set; }


    }
}
