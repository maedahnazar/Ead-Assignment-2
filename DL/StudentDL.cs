using Core.Interfaces;
using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using DL.DbModels;
using System;
using DL;
using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace DL

{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class StudentDL : IStudentDL
    {
        private readonly StudentDbContext _stContext;

        public StudentDL(StudentDbContext stContext)
        {
            _stContext = stContext;
        }

        public StudentDetailSubjectResponseDto GetStudent(int id)
        {
            var student = _stContext.studentDbDto
                .Where(s => s.Id == id)
                .Select(s => new StudentDetailSubjectResponseDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    RollNo = s.RollNumber,
                    PhoneNumber = s.PhoneNumber,
                    SubjectMarks = s.StudentSubjects
                        .Select(ss => new StudentSubjectMarksDto
                        {
                            // Map properties from StudentSubjectDbDto to StudentSubjectMarksDto
                            // Adjust this mapping based on your data model
                        })
                        .ToList()
                })
                .FirstOrDefault();

            if (student == null)
            {
                // Handle the case where the student with the given ID is not found
                // You may throw an exception, log an error, or handle it based on your requirements
                throw new DL.NotFoundException("Student not found");
            }

            return student;
        }


        public IEnumerable<StudentResponseDto> GetStudents()
        {
            // Retrieve all students from the database and map them to StudentResponseDto
            return _stContext.studentDbDto.AsEnumerable().Select(student => new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                RollNo = student.RollNumber,
                // Map other properties accordingly
            });
        }

        public IEnumerable<StudentResponseDto> GetStudentsAsync()
        {
            // You need to implement asynchronous logic to retrieve students
            throw new NotImplementedException();
        }

        public StudentResponseDto SaveStudent(StudentRequestDto studentRequestDto)
        {
            // Map the properties from StudentRequestDto to StudentDbDto
            var newStudent = new StudentDbDto
            {
                Name = studentRequestDto.Name,
                RollNumber = studentRequestDto.RollNo,
                PhoneNumber = studentRequestDto.PhoneNumber
            };

            // Add the new student to the DbContext and save changes
            _stContext.studentDbDto.Add(newStudent);
            _stContext.SaveChanges();

            // Map the newly added student to the response DTO
            var responseDto = new StudentResponseDto
            {
                Id = newStudent.Id,
                Name = newStudent.Name,
                RollNo = newStudent.RollNumber,

                PhoneNumber = newStudent.PhoneNumber
                // ... other properties
            };

            return responseDto;
        }


        public StudentResponseDto UpdateStudent(int studentId, StudentRequestDto studentRequestDto)
        {
            var existingStudent = _stContext.studentDbDto.FirstOrDefault(s => s.Id == studentId);

            if (existingStudent == null)
            {
                // Handle the case where the student with the given ID is not found
                // You may throw an exception, log an error, or handle it based on your requirements
                throw new DL.NotFoundException("Student not found");
            }

            // Update properties of the existing student based on the request DTO
            existingStudent.Name = studentRequestDto.Name;
            existingStudent.RollNumber = studentRequestDto.RollNo;
            existingStudent.PhoneNumber = studentRequestDto.PhoneNumber;
            // ... update other properties

            // Save changes to the database
            _stContext.SaveChanges();

            // Assuming StudentResponseDto has a constructor that takes a StudentDbDto
            return new StudentResponseDto(existingStudent.Id, existingStudent.Name, existingStudent.RollNumber, existingStudent.PhoneNumber);
        }


        public StudentResponseDto DeleteStudent(int studentId)
        {
            var existingStudent = _stContext.studentDbDto.FirstOrDefault(s => s.Id == studentId);

            if (existingStudent == null)
            {
                throw new DL.NotFoundException("Student not found");
            }

            // Remove the student from the context and save changes
            _stContext.studentDbDto.Remove(existingStudent);
            _stContext.SaveChanges();

            // Assuming StudentResponseDto has a constructor that takes a StudentDbDto
            return new StudentResponseDto(
                existingStudent.Id, existingStudent.Name, existingStudent.RollNumber, existingStudent.PhoneNumber);
        }


    }
}
