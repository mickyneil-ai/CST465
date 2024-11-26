using System.ComponentModel.DataAnnotations;

namespace Midterm
{
    public class MultipleChoiceQuestion : TestQuestionModel
    {
        [Required]
        public override string Answer {  get; set; }
        public List<string> choices { get; set; } = new List<string>();
    }
}
