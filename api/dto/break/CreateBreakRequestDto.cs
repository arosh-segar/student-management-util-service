using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.dto.session
{
    public class CreateBreakRequestDto
    {
        [Required(ErrorMessage = "Created Date is required")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 minute")]
        public int Duration { get; set; }
    }
}