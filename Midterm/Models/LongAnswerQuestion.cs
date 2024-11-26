using System.ComponentModel.DataAnnotations;

namespace Midterm
{
    public class LongAnswerQuestion : TestQuestionModel
    {
        [Required]
        public override string Answer {  get; set; }
    }
}
