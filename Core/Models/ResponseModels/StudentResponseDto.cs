using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.ResponseModels
{
    public class StudentResponseDto : IStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RollNo { get; set; }
        //public double GPA { get; set; }
        public string PhoneNumber { get; set; }
        public StudentResponseDto() {}
        public StudentResponseDto(int id, string name, string rollNo, string phoneNumber)
        {
            Id = id;
            Name = name;
            RollNo = rollNo;
            PhoneNumber = phoneNumber;
        }
    }

    public class StudentDetailSubjectResponseDto : StudentResponseDto
    {
        public List<StudentSubjectMarksDto> SubjectMarks { get; set; }
    }
}
