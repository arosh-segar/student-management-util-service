using System.ComponentModel.DataAnnotations;

namespace api.dto.subject
{
    public class UpdateSubjectRequestDto
    {
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string? Name { get; set; }

        [Range(1, 100, ErrorMessage = "Knowledge Level must be between 1 and 100")]
        public int? KnowledgeLevel { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of Chapters must be at least 1")]
        public int? NumberOfChapters { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Chapters Covered must be at least 0")]
        public int? NumberOfChaptersCovered { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }

    }
}