using System.ComponentModel.DataAnnotations.Schema;

namespace Login_And_Registration.Models
{
    public class TimeperiodViewModel
    {


        public int TimePeriodId { get; set; }

        
        public TimeSpan StartTime { get; set; }

    
        public TimeSpan EndTime { get; set; }
    }
}
