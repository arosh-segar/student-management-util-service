using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models
{
    public class Break
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Duration { get; set; }

        public string? UserEmail { get; set; }
    }
}
