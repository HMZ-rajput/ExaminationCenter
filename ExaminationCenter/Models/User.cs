using System.ComponentModel.DataAnnotations;

namespace ExaminationCenter.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Role { get; set; }
    }
}
