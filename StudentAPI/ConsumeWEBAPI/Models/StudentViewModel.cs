using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumeWEBAPI.Models
{
    public class StudentViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Studen Name")]
        public string Name { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }

        //public string Address { get; set; }

    }
}
