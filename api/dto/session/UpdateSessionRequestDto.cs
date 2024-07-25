using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.dto.session
{
    public class UpdateSessionRequestDto
    {
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 minute")]
        public int? Duration { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Chapter covered count must be greater than or equal to 0")]
        public int? ChaptersCovered { get; set; }
    }
}