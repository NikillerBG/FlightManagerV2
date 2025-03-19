using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "EGN must be 10 digits.")]
        public string EGN { get; set; }

        public string Address { get; set; }

        [Required]
        public Role Role { get; set; }

        public User()
        {
            
        }

        public User(string firstName, string lastName, string egn, string address, Role role)
        {
            Id = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            EGN = egn;
            Address = address;
            Role = role;
        }
    }
}
