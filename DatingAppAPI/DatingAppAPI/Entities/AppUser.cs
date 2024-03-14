using DatingAppAPI.Extenstions;
using System.ComponentModel.DataAnnotations;

namespace DatingAppAPI.Entities
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> Photos { get; set; } = new();

        public string DateOfBirthFormatted => DateOfBirth.ToString("yyyy-MM-dd");

        // 'Get' is important for AutoMapper -> public int Age { get; set; }
        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}
    }
}
