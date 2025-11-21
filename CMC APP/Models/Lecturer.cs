using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }
        public int Id { get; set; }
        public string Phone { get; set; }
        [Required] public string Name { get; set; }
        [Required][EmailAddress] public string Email { get; set; }

        public List<Claim> Claims { get; set; } = new List<Claim>();
    }
}
