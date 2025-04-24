using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Models
{
    public class Faculty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FacultyName { get; set; }
        public int Priority { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }
        public virtual University? University { get; set; }

        public bool IsDeleted { get; set; } = false;


        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
