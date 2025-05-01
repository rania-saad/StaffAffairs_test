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
    public class FacultyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Faculty name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name must be 3-255 characters")]
        public string FacultyName { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Priority must be 0-100")]
        public int Priority { get; set; } = 0;

        [Required(ErrorMessage = "University ID is required")]
        public int UniversityId { get; set; }
    }
    public class updateFacultyDTO

    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Faculty name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name must be 3-255 characters")]
        public string FacultyName { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Priority must be 0-100")]
        public int Priority { get; set; } = 0;

        [Required(ErrorMessage = "University ID is required")]
        public int UniversityId { get; set; }

        public List<int>? DepartmentIds { get; set; }



    }
}
