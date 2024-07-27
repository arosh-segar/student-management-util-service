using api.dto.prediction;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [Route("api/prediction")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IBreakRepository _breakRepository;
        private readonly ISubjectRepository _subjectRepository;

        public PredictionController(ISessionRepository sessionRepository, IBreakRepository breakRepository, ISubjectRepository subjectRepository)
        {
            _sessionRepository = sessionRepository;
            _breakRepository = breakRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetPrediction([FromBody] PredictionCreateDto predictionCreateDto)
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

            var subject = await _subjectRepository.GetByIdAsync(predictionCreateDto.SubjectId, email);
            if (subject == null) return NotFound();

            var sessions = await _sessionRepository.GetSessionsBySubjectId(predictionCreateDto.SubjectId, email);

            var totalStudyMinutes = 0;

            // Remaining chapters
            var remainingChapters = subject.NumberOfChapters - subject.NumberOfChaptersCovered;
            var totalDaysStudied = 0.0;

            if (sessions.Any())
            {
                totalStudyMinutes = sessions.Sum(s => s.Duration);
                remainingChapters = remainingChapters - sessions.Sum(s => s.ChaptersCovered);
                totalDaysStudied = sessions.GroupBy(s => s.CreatedDate.Date).ToList().Count;
            }

            // Average study time per day
            var averageDailyStudyMinutes = totalDaysStudied > 0 ? (totalStudyMinutes / totalDaysStudied) : 0;

            // Days left until deadline
            var daysLeft = (subject.Deadline - DateTime.Now).TotalDays;

            var hoursPerChapter = 6;

            switch (subject.KnowledgeLevel)
            {
                case int k when k > 80 && k <= 100:
                    hoursPerChapter = 2;
                    break;
                case int k when k > 60 && k <= 80:
                    hoursPerChapter = 3;
                    break;
                case int k when k > 40 && k <= 60:
                    hoursPerChapter = 4;
                    break;
                case int k when k > 20 && k <= 40:
                    hoursPerChapter = 5;
                    break;
                default:
                    hoursPerChapter = 6;
                    break;
            }

            var requiredTimeToCompleteInMinutes = hoursPerChapter * remainingChapters * 60;

            // Required study time per day to finish on time
            var requiredDailyStudyMinutes = daysLeft > 0 ? (requiredTimeToCompleteInMinutes) / daysLeft : 0;
            var predictedKnowledgeLevel = subject.KnowledgeLevel + (subject.KnowledgeLevel * 20) / 100;

            var requiredDailyStudyhours = Math.Ceiling(requiredDailyStudyMinutes / 60);

            var prediction = new PredictionDto
            {
                SubjectName = subject.Name,
                RequiredDailyStudyMinutes = requiredDailyStudyhours,
                AverageDailyStudyMinutes = averageDailyStudyMinutes,
                PredictedKnowledgeLevel = predictedKnowledgeLevel > 100 ? 100 : predictedKnowledgeLevel, // Simplified prediction
                SelectedSubjectId = predictionCreateDto.SubjectId,
                IsGenerated = true,
                ChaptersLeftToCover = remainingChapters
            };

            return Ok(prediction);
        }
    }
}