// DL/DbModels/StudentDbDto.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DL.DbModels
{
    public class StudentDbDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RollNumber { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<StudentSubjectDbDto> StudentSubjects { get; set; } = new List<StudentSubjectDbDto>();
       // public double GPA { get; internal set; }
    }
}
