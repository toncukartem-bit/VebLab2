using System;

namespace VebLab2.Models
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; } // Nullable
        public DateTime? BirthDate { get; set; } // Nullable
        public string PhoneNumber { get; set; }
        public double? ExperienceYears { get; set; } // Nullable
        public string Position { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}