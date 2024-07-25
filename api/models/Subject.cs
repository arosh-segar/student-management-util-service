using System.ComponentModel.DataAnnotations;
using StudentManagementApp.Models;

namespace api.models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int KnowledgeLevel { get; set; }

        public int NumberOfChapters { get; set; }

        public int NumberOfChaptersCovered { get; set; }

        public DateTime Deadline { get; set; }

        public string? UserEmail { get; set; }

        public List<Session> Sessions { get; set; } = new List<Session>();
    }
}