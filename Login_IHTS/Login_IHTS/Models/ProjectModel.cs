using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_TimeTracker.Models
{
    public class ProjectModel
    {
        [Key]
        [Column("PROJECTNAMEID")]
        public int ProjectId { get; set; }

        [Column("PROJECTNAME")]
        public string ProjectName { get; set; }



       
    }

}
