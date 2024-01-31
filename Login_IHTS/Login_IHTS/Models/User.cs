using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace API_TimeTracker.Models
{
    public class User
    {
        [Column("USERID")]
        public int UserId { get; set; }

        [Column("USERNAME")]
        public string UserName { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [Column("PASSWORD")]
        public string Password { get; set; } = string.Empty;


        [Column("PERMISSION")]
        public byte permission { get; set; }






    }

}
