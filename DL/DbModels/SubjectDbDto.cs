// DL/DbModels/SubjectDbDto.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DL.DbModels
{
    public class SubjectDbDto
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }

        public ICollection<StudentSubjectDbDto> StudentSubjects { get; set; } = new List<StudentSubjectDbDto>();
    }
}
