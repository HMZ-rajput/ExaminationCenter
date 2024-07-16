using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationCenter.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Identity {  get; set; }

        public string Position { get; set; }

        public string Status { get; set; }

        public DateTime DateApplied { get; set; }

        public byte[] UserImage { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
