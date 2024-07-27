namespace api.dto.prediction
{
    public class PredictionDto
    {
        public string SubjectName { get; set; }
        public double RequiredDailyStudyMinutes { get; set; }
        public double AverageDailyStudyMinutes { get; set; }
        public int PredictedKnowledgeLevel { get; set; }
        public int SelectedSubjectId { get; set; }
        public bool IsGenerated { get; set; }
        public int ChaptersLeftToCover { get; set; }
    }
}