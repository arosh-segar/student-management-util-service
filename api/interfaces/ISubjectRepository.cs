using api.dto.subject;
using api.models;

namespace api.interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllByUserEmailAsync(string userEmail);

        Task<Subject?> GetByIdAsync(int id, string userId);

        Task<Subject> CreateAsync(Subject subject);

        Task<Subject?> UpdateAsync(int id, UpdateSubjectRequestDto subjectRequestDto, string userEmail);

        Task<Subject?> DeleteAsync(int id, string userEmail);
    }
}