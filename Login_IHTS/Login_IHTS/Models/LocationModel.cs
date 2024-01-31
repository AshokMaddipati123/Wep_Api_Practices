using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_TimeTracker.Models
{
    public class LocationModel
    {
        [Key]
        [Column("LOCATIONID")]
        public int LocationId { get; set; }

        [Column("LOCATIONNAME")]
        public string LocationName { get; set; }

    }

}
