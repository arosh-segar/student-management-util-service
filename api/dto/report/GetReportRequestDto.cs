using System.ComponentModel.DataAnnotations;

namespace api.dto.report
{
    public class GetReportRequestDto
    {
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int? subjectId { get; set; }
    }
}