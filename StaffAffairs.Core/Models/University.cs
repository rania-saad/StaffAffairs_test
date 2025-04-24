using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Models
{
    public class University
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string UniversityName { get; set; }
        public bool? Foreign_University { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
        //public DateTime? DeletedDate { get; set; }
       // public string DeletedBy { get; set; }


    }
}
