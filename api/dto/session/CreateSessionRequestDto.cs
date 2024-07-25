using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.dto.session
{
    public class CreateSessionRequestDto
    {
        [Required(ErrorMessage = "Subject is required")]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 minute")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Chapters Covered is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Chapter covered count must be greater than or equal to 0")]
        public int ChaptersCovered { get; set; }
    }
}