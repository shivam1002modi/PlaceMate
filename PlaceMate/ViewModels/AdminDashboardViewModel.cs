using PlaceMate.Models;
using System.Collections.Generic;

namespace PlaceMate.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalExperiences { get; set; }
        public int PendingExperiences { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalStudents { get; set; }
        public Dictionary<string, int> TopCompanies { get; set; }
    }
}