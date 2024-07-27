using api.dto.session;
using StudentManagementApp.Models;

namespace api.interfaces
{
    public interface IBreakRepository
    {
        Task<List<Break>> GetAllByUserEmailAsync(string userEmail);

        Task<Break?> GetByIdAsync(int id, string userId);

        Task<Break> CreateAsync(Break breakObject);

        Task<Break?> UpdateAsync(int id, UpdateBreakRequestDto updateBreakRequestDto, string userEmail);

        Task<Break?> DeleteAsync(int id, string userEmail);

        Task<List<Break>> GetBreaksByPeriod(DateTime startDate, DateTime endDate, string userEmail);
    }
}