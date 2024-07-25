using api.data;
using api.dto.session;
using api.interfaces;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace api.repositories
{
    public class BreakRepository : IBreakRepository
    {
        private readonly ApplicationDBContext _context;

        public BreakRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Break> CreateAsync(Break breakObject)
        {
            await _context.Break.AddAsync(breakObject);
            await _context.SaveChangesAsync();
            return breakObject;
        }

        public async Task<Break?> DeleteAsync(int id, string userEmail)
        {
            var breaKModel = await _context.Break.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();

            if (breaKModel == null)
            {
                return null;
            }

            _context.Break.Remove(breaKModel);

            await _context.SaveChangesAsync();

            return breaKModel;
        }

        public async Task<List<Break>> GetAllByUserEmailAsync(string userEmail)
        {
            var sessions = await _context.Break.Where(s => s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).ToListAsync();

            return sessions;
        }

        public async Task<Break?> GetByIdAsync(int id, string userEmail)
        {
            return await _context.Break.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();
        }

        public async Task<Break?> UpdateAsync(int id, UpdateBreakRequestDto updateBreakRequestDto, string userEmail)
        {
            var breaKModel = await _context.Break.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();

            if (breaKModel == null)
            {
                return null;
            }

            breaKModel.CreatedDate = updateBreakRequestDto.CreatedDate != null ? updateBreakRequestDto.CreatedDate : breaKModel.CreatedDate;
            breaKModel.Duration = updateBreakRequestDto.Duration != null ? updateBreakRequestDto.Duration : breaKModel.Duration;

            await _context.SaveChangesAsync();

            return breaKModel;
        }

    }
}