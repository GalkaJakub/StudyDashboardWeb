namespace StudyDS_web.Models.ViewModels
{
    public class StatsViewModel
    {
        public int TotalEcts { get; set; }
        public int PassedEcts { get; set; }
        public int SubjectCount { get; set; }
        public int SubjectsPassed { get; set; }
        public int EventCount { get; set; }
        public int EventsPassed { get; set; }
        public double AverageSubjectsGrade { get; set; }
        public double AverageEventsGrade { get; set; }
    }
}
