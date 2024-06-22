
using System.ComponentModel.DataAnnotations;

namespace BookShope.Models;
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string User_Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a first name.")]
        public string first_Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a last name.")]
        public string last_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; } = string.Empty;

        public int Flag { get; set; } = 0;
    }
}
