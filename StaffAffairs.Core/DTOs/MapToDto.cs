//using StaffAffairs.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StaffAffairs.Core.DTOs
//{
//    private FacultyDTO MapToDto(Faculty faculty)
//    {
//        return new FacultyDTO
//        {
//            Id = faculty.Id,
//            FacultyName = faculty.FacultyName,
//            Priority = faculty.Priority,
//            UniversityId = faculty.UniversityId,
//            DepartmentIds = faculty.Departments?.Select(d => d.Id).ToList() ?? new List<int>()
//        };
//    }
//}
