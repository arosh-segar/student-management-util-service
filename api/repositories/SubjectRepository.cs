using api.data;
using api.dto.subject;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDBContext _context;
        public SubjectRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Subject> CreateAsync(Subject subject)
        {
            await _context.Subject.AddAsync(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject?> DeleteAsync(int id, string userEmail)
        {
            var subjectModel = await _context.Subject.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();

            if (subjectModel == null)
            {
                return null;
            }

            _context.Subject.Remove(subjectModel);

            await _context.SaveChangesAsync();

            return subjectModel;
        }


        public async Task<List<Subject>> GetAllByUserEmailAsync(string userEmail)
        {
            var subjects = await _context.Subject.Where(s => s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).ToListAsync();

            return subjects;
        }

        public async Task<Subject?> GetByIdAsync(int id, string userEmail)
        {
            return await _context.Subject.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();
        }

        public async Task<Subject?> UpdateAsync(int id, UpdateSubjectRequestDto subjectRequestDto, string userEmail)
        {
            var subjectModel = await _context.Subject.Where(s => s.Id == id && s.UserEmail.ToUpper().Equals(userEmail.ToUpper())).FirstOrDefaultAsync();

            if (subjectModel == null)
            {
                return null;
            }

            subjectModel.Name = subjectRequestDto.Name != null ? subjectRequestDto.Name : subjectModel.Name;
            subjectModel.KnowledgeLevel = (int)(subjectRequestDto.KnowledgeLevel != null ? subjectRequestDto.KnowledgeLevel : subjectModel.KnowledgeLevel);
            subjectModel.NumberOfChapters = (int)(subjectRequestDto.NumberOfChapters != null ? subjectRequestDto.NumberOfChapters : subjectModel.NumberOfChapters);
            subjectModel.NumberOfChaptersCovered = (int)(subjectRequestDto.NumberOfChaptersCovered != null ? subjectRequestDto.NumberOfChaptersCovered : subjectModel.NumberOfChaptersCovered);
            subjectModel.Deadline = (DateTime)(subjectRequestDto.Deadline != null ? subjectRequestDto.Deadline : subjectModel.Deadline);

            await _context.SaveChangesAsync();

            return subjectModel;
        }
    }
}