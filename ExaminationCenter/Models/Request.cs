using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationCenter.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Identity {  get; set; }

        public string Position { get; set; }

        public string Status { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateTime DateApplied { get; set; }

        public string UserImage { get; set; }
    }
}
