using api.dto.session;
using StudentManagementApp.Models;

namespace api.interfaces
{
    public interface ISessionRepository
    {
        Task<List<Session>> GetAllByUserEmailAsync(string userEmail);

        Task<Session?> GetByIdAsync(int id, string userId);

        Task<Session> CreateAsync(Session session);

        Task<Session?> UpdateAsync(int id, UpdateSessionRequestDto updateSessionRequestDto, string userEmail);

        Task<Session?> DeleteAsync(int id, string userEmail);

        Task<List<Session>> GetSessionsByPeriodAndSubject(DateTime startDate, DateTime endDate, int? subjectId, string userEmail);

        Task<List<Session>> GetSessionsBySubjectId(int subjectId, string userEmail);
    }
}