using api.dto.report;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;

namespace api.controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IBreakRepository _breakRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ReportController(ISessionRepository sessionRepository, IBreakRepository breakRepository, ISubjectRepository subjectRepository)
        {
            _sessionRepository = sessionRepository;
            _breakRepository = breakRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReports([FromBody] GetReportRequestDto report)
        {
            var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;

            var email = "";
            if (claimsIdentity != null)
            {
                email = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            }
            else
            {
                Unauthorized("Not Authorized");
            }

            var sessions = await _sessionRepository.GetSessionsByPeriodAndSubject(report.StartDate, report.EndDate, report.subjectId, email);
            var breaks = await _breakRepository.GetBreaksByPeriod(report.StartDate, report.EndDate, email);
            var subjects = await _subjectRepository.GetAllByUserEmailAsync(email);

            // Aggregate sessions by date and subject, and sum the durations
            var aggregatedSessions = sessions
                .GroupBy(s => new { s.CreatedDate.Date, s.SubjectId })
                .Select(g => new Session
                {
                    CreatedDate = g.Key.Date,
                    Duration = g.Sum(s => s.Duration),
                    Subject = g.First().Subject,
                    SubjectId = g.Key.SubjectId
                })
                .ToList();

            // Aggregate breaks by date and sum the durations
            var aggregatedBreaks = breaks
                .GroupBy(b => b.CreatedDate.Date)
                .Select(g => new Break
                {
                    CreatedDate = g.Key,
                    Duration = g.Sum(b => b.Duration)
                })
                .ToList();

            var reportResponse = new ReportResponseDto
            {
                Sessions = aggregatedSessions,
                Breaks = aggregatedBreaks,
            };

            return Ok(reportResponse);
        }
    }
}