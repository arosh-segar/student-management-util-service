using api.data;
using api.dto.session;
using api.interfaces;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace api.repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDBContext _context;

        public SessionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Session> CreateAsync(Session session)
        {
            await _context.Session.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<Session?> DeleteAsync(int id, string userEmail)
        {
            var sessionModel = await _context.Session.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();

            if (sessionModel == null)
            {
                return null;
            }

            _context.Session.Remove(sessionModel);

            await _context.SaveChangesAsync();

            return sessionModel;
        }

        public async Task<List<Session>> GetAllByUserEmailAsync(string userEmail)
        {
            return await _context.Session.Include(s => s.Subject) // Include the Subject entity
                .Where(s => s.UserEmail.ToUpper() == userEmail.ToUpper())
                .ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id, string userEmail)
        {
            return await _context.Session.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();
        }

        public async Task<Session?> UpdateAsync(int id, UpdateSessionRequestDto updateSessionRequestDto, string userEmail)
        {
            var sessionModel = await _context.Session.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();

            if (sessionModel == null)
            {
                return null;
            }

            sessionModel.SubjectId = (int)(updateSessionRequestDto.SubjectId != null ? updateSessionRequestDto.SubjectId : sessionModel.SubjectId);
            sessionModel.CreatedDate = (DateTime)(updateSessionRequestDto.CreatedDate != null ? updateSessionRequestDto.CreatedDate : sessionModel.CreatedDate);
            sessionModel.Duration = (int)(updateSessionRequestDto.Duration != null ? updateSessionRequestDto.Duration : sessionModel.Duration);
            sessionModel.ChaptersCovered = (int)(updateSessionRequestDto.ChaptersCovered != null ? updateSessionRequestDto.ChaptersCovered : sessionModel.ChaptersCovered);

            await _context.SaveChangesAsync();

            return sessionModel;
        }

    }
}