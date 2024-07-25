using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.models;

namespace StudentManagementApp.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Duration { get; set; }

        public int ChaptersCovered { get; set; }

        public string? UserEmail { get; set; }
    }
}
