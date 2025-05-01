using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffAffairs.Core.Interfaces;

namespace StaffAffairs.Core.Models
{
    public class Department : IEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string DepartmentName { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public virtual Faculty? Faculty { get; set; }

        public bool IsDeleted { get; set; } = false;


        public void Delete()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
