using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_TimeTracker.Models
{
    public class TaskModel
    {
        [Key]
        [Column("ID")]
        public int TaskId { get; set; }

        [Column("USERID")]
        public int UserId { get; set; }

        [Column("USERSTORYORBUGNO")]
        public string UserStoryBugNumber { get; set; }

        [Column("PROJECTNAMEID")]
        public int ProjectId { get; set; }

        [Column("STARTTIME")]
        public TimeOnly StartTime { get; set; }

        [Column("ENDTIME")]
        public TimeOnly EndTime { get; set; }


        [Column("LOCATIONID")]
        public int LocationId { get; set; }

        [Column("TASKDESCRIPTION")]
        public string TaskDescription { get; set; }

     
        [Column("CREATIONDATE")]
        public DateOnly CREATIONDATE { get; set; }

        //[NotMapped]
        //public string UserName { get; set; }



    }

}
