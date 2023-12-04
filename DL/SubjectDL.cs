using Core.Interfaces;
using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using DL.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DL
{
    public class SubjectDL : ISubjectDL
    {
        private readonly StudentDbContext _context;

        public SubjectDL(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<SubjectResponseDto> CreateSubject(SubjectRequestDto subjectRequestDto)
        {
            if (subjectRequestDto == null)
            {
                throw new ArgumentNullException(nameof(subjectRequestDto));
            }

            var subjectDbDto = new SubjectDbDto
            {
                Name = subjectRequestDto.Name
            };

            _context.Subjects.Add(subjectDbDto);
            await _context.SaveChangesAsync();

            return MapToResponseDto(subjectDbDto);
        }

        public async Task<SubjectResponseDto> UpdateSubject(int id, SubjectRequestDto updatedSubjectRequestDto)
        {
            if (updatedSubjectRequestDto == null)
            {
                throw new ArgumentNullException(nameof(updatedSubjectRequestDto));
            }

            var existingSubject = await _context.Subjects.FindAsync(id);

            if (existingSubject == null)
            {
                return null; // Or return NotFound() or throw an exception
            }

            existingSubject.Name = updatedSubjectRequestDto.Name;

            _context.Entry(existingSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception if needed
                throw;
            }

            return MapToResponseDto(existingSubject);
        }

        public async Task<bool> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return false; // Or return NotFound() or throw an exception
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<SubjectResponseDto>> GetAllSubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();

            return subjects.Select(MapToResponseDto);
        }

        public async Task<SubjectResponseDto> GetSubjectById(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            return subject != null ? MapToResponseDto(subject) : null;
        }

        private static SubjectResponseDto MapToResponseDto(SubjectDbDto subjectDbDto)
        {
            return new SubjectResponseDto
            {
                Id = subjectDbDto.id,
                Name = subjectDbDto.Name
                // Add other properties as needed
            };
        }
    }
}
