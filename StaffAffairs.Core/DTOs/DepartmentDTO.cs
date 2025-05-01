using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.DTOs
{
    public class DepartmentDTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
    }
}
