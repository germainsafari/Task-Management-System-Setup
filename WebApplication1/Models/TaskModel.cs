using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string AssignedUser { get; set; }

        [Required]
        public string Priority { get; set; } // High, Medium, Low

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
