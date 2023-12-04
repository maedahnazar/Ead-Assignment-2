using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISubjectDL
    {
        Task<SubjectResponseDto> CreateSubject(SubjectRequestDto subjectRequestDto);
        Task<SubjectResponseDto> UpdateSubject(int id, SubjectRequestDto updatedSubjectRequestDto);
        Task<bool> DeleteSubject(int id);
        Task<IEnumerable<SubjectResponseDto>> GetAllSubjects();
        Task<SubjectResponseDto> GetSubjectById(int id);
    }
}
