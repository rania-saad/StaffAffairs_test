using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.DTOs
{
    public class JobDTO
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

        [ForeignKey("EntedabType")]
        public int EntedabTypeId { get; set; }
    }

    public class updateJobDTO

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string JobName { get; set; }

        public int? JobPriority { get; set; }


        public int JobTypeId { get; set; }

        public int EntedabTypeId { get; set; }



    }
}
