using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Project.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }
    }
}
