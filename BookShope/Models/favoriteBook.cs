using System.ComponentModel.DataAnnotations;

namespace BookShope.Models 
{
    public class favoriteBook
    {
        [Key]

        [Required(ErrorMessage = "Please enter a book ID.")]
        public int? Book_Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Book_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a writer.")]
        public string Writer { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter a date.")]
        public int price { get; set; } = 0;


        public User User { get; set; } = null!;
        public int? User_Id { get; set; }
    }
}
