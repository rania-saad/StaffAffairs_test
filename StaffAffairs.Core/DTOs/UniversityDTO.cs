using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.DTOs
{
    public class UniversityDTO
    {
        public int Id { get; set; }
        public string UniversityName { get; set; }
        public bool? Foreign_University { get; set; }
    }
}
