//using StaffAffairs.Core.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StaffAffairs.Core.Models
//{
//    public class University : IEntity, ISoftDeletable 
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        public int Id { get; set; }

//        [Required]
//        [MaxLength(255)]
//        public string UniversityName { get; set; }
//        public bool? Foreign_University { get; set; } = false;

//        public bool IsDeleted { get; private set; } = false;
//        //public DateTime? DeletedDate { get; set; }
//       // public string DeletedBy { get; set; }


//    }
//}

using StaffAffairs.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class University : IEntity, ISoftDeletable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string UniversityName { get; set; }
    public bool? Foreign_University { get; set; } = false;

    public bool IsDeleted { get; private set; } = false;

    public void Delete()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}