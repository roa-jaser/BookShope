using System.ComponentModel.DataAnnotations;

namespace BookShope.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }


        [Required(ErrorMessage = "Please enter a name.")]
        public string User_Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; } = string.Empty;
        public int Flag { get; set; } = 1;
    }
}
