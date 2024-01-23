namespace API_TimeTracker.Models
{
    public class TimePeriodModel
    {
        public int TimePeriodId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
