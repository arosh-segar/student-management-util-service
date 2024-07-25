using System.ComponentModel.DataAnnotations;

namespace api.dto.session
{
    public class UpdateBreakRequestDto
    {
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 minute")]
        public int Duration { get; set; }
    }
}