using System.ComponentModel.DataAnnotations;

namespace Lab8.Models
{
    public class ImageModel
    {
        [Required(ErrorMessage = "Please upload a file.")]
        public IFormFile File { get; set; } 

        [Required(ErrorMessage = "Please provide a description.")]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; } 
    }
}
