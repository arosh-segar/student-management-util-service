using StudentManagementApp.Models;

namespace api.dto.report
{
    public class ReportResponseDto
    {
        public required List<Break> Breaks { get; set; }
        public required List<Session> Sessions { get; set; }
    }
}