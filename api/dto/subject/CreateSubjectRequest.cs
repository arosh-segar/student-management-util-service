using System.ComponentModel.DataAnnotations;

namespace api.dto
{
    public class CreateSubjectRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Knowledge Level is required")]
        [Range(1, 100, ErrorMessage = "Knowledge Level must be between 1 and 100")]
        public int KnowledgeLevel { get; set; }

        [Required(ErrorMessage = "Number of Chapters is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Chapters must be at least 1")]
        public int NumberOfChapters { get; set; }

        [Required(ErrorMessage = "Number of Chapters Covered is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of Chapters Covered must be at least 0")]
        public int NumberOfChaptersCovered { get; set; }

        [Required(ErrorMessage = "Deadline is required")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
    }
}