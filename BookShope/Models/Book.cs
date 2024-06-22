using System.ComponentModel.DataAnnotations;

namespace BookShope.Models 
{
    public class Book
    {
        [Key]
        public int? Book_Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Book_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a writer.")]
        public string Writer { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter a price.")]
        public int price { get; set; } = 0;
         
    }
}
