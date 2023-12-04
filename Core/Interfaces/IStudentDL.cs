using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IStudentDL
    {
        StudentResponseDto SaveStudent(StudentRequestDto studentRequestDto);
        StudentDetailSubjectResponseDto GetStudent(int id);
        IEnumerable<StudentResponseDto> GetStudents(); // Corrected the return type here
        public StudentResponseDto UpdateStudent(int studentId, StudentRequestDto studentRequestDto);
        StudentResponseDto DeleteStudent(int studentId);
    }
}
