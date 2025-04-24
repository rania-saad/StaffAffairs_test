using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Models
{
    public class EntedabType
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public bool EntedabTypeName { get; set; } = false;
        public ICollection<Job>? Jobs { get; set; }
    }
}
