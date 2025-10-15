using System.ComponentModel.DataAnnotations;

namespace PlaceMate.Models
{
    public class InterviewExperience
    {
        [Key]
        public int ExperienceId { get; set; }

        [Required]
        public string JobRole { get; set; }

        [Required]
        public string TechnicalQuestions { get; set; }

        [Required]
        public string HRQuestions { get; set; }

        public string TipsForJuniors { get; set; }

        [Range(1, 5)]
        [Display(Name = "Interview Difficulty (1=Easy, 5=Hard)")]
        public int InterviewDifficulty { get; set; }

        public int YearOfInterview { get; set; }

        public bool IsApproved { get; set; }

        //foreignkeys

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}