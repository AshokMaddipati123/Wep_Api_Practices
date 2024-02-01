using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_TimeTracker.Models
{
    public class TaskViewModel
    {

        public int TaskId { get; set; }

      
        public int UserId { get; set; }

        
        public string UserStoryBugNumber { get; set; }

        public int ProjectId { get; set; }

       
        public TimeOnly StartTime { get; set; }

       
        public TimeOnly EndTime { get; set; }


    
        public int LocationId { get; set; }
        public string TaskDescription { get; set; }


        public DateOnly CREATIONDATE { get; set; }



       

    }
}
