using System.Collections.Generic;

namespace PlaceMate.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyWebsite { get; set; }

        // Navigation property
        public ICollection<InterviewExperience> InterviewExperiences { get; set; }
    }
}
