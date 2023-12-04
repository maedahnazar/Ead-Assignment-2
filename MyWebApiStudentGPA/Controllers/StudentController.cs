using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using System.Collections.Generic;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentDL _studentDL;

        public StudentsController(IStudentDL studentDL)
        {
            _studentDL = studentDL;
        }

        // POST /students
        [HttpPost]
        public ActionResult<StudentResponseDto> CreateStudent(StudentRequestDto studentRequestDto)
        {
            var createdStudent = _studentDL.SaveStudent(studentRequestDto);
            return CreatedAtAction(nameof(GetStudent), new { studentId = createdStudent.Id }, createdStudent);
        }

        // PUT /students/{student_id}
        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(int studentId, StudentRequestDto studentRequestDto)
        {
            var updatedStudent = _studentDL.UpdateStudent(studentId, studentRequestDto);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            return Ok(updatedStudent);
        }

        // DELETE /students/{student_id}
        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            var deletedStudent = _studentDL.DeleteStudent(studentId);
            if (deletedStudent == null)
            {
                return NotFound();
            }
            return Ok(deletedStudent);
        }

        // GET /students/{student_id}
        [HttpGet("{studentId}")]
        public ActionResult<StudentResponseDto> GetStudent(int studentId)
        {
            var student = _studentDL.GetStudent(studentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // GET /students
        [HttpGet]
        public ActionResult<IEnumerable<StudentResponseDto>> GetStudents()
        {
            var students = _studentDL.GetStudents();
            return Ok(students);
        }

        // Additional Endpoints for the Student API (as per the provided tasks)
        // Implement other actions based on the tasks for Student-Subject Assignment, Marks, and GPA
        // ...

        // Feel free to add more actions as needed for your requirements
    }
}
