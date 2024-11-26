using System.ComponentModel.DataAnnotations;

namespace Midterm
{
    public class ShortAnswerQuestion : TestQuestionModel
    {
        const int ansLim = 100;
        [Required]
        [StringLength(ansLim, ErrorMessage = "Answer must be {1} characters or fewer.")]
        public override string Answer { get; set; }
        
    }
}
