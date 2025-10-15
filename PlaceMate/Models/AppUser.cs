using System.Collections.Generic;

namespace PlaceMate.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        // Navigation property: One user can have many interview experiences
        public ICollection<InterviewExperience> InterviewExperiences { get; set; }
    }
}
