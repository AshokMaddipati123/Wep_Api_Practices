
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentAPI.Model
{
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Studen Name")]
        public string Name { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }

      
    }
}
