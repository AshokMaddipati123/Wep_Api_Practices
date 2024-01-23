namespace MVC_TimeTracker.Models
{
    public class TaskViewModel
    {

        public int TaskId { get; set; }
        public string UserStoryBugNumber { get; set; }
        public int ProjectId { get; set; }
        public string TaskDescription { get; set; }
        public int TimePeriodId { get; set; }
        public DateTime TaskDate { get; set; }
        public int LocationId { get; set; }
    }
}
