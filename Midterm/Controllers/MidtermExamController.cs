using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.IO;
namespace Midterm;

public class MidtermExamController : Controller
{
    private readonly IConfiguration _Config;
    private readonly string _JsonFilePath;

    public MidtermExamController(IConfiguration conf)
    {
        _Config = conf;
        _JsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "JSON", "MidtermQuestions.json");
    }

    [Route("")]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [Route("TakeTest")]
    [HttpGet]
    public IActionResult TakeTest()
    {
        List<TestQuestionModel> questionModels = GetQuestionModels();
        return View(questionModels);
    }

    [Route("SubmitTest")]
    [HttpPost]
    public IActionResult SubmitTest(List<TestQuestionModel> model)
    {
        List<TestQuestionModel> questionModels = GetQuestionModels();
        foreach (var postedAnswer in model)
        {
            var matchingQuestion = questionModels.FirstOrDefault(q => q.id == postedAnswer.id);
            if (matchingQuestion != null)
            {
                matchingQuestion.Answer = postedAnswer.Answer;
            }
        }
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Validation errors ignored for unanswered questions.");
        }
        return View("DisplayResults", questionModels);
    }
    
    private List<TestQuestionModel> GetQuestionModels()
    {
        if (!System.IO.File.Exists(_JsonFilePath))
        {
            throw new FileNotFoundException("The MidtermQuestions.json file could not be found.");
        }

        string jsonContent = System.IO.File.ReadAllText(_JsonFilePath);
        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(jsonContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        List<TestQuestionModel> questionModels = new List<TestQuestionModel>();
        foreach (var question in questions)
        {
            switch (question.QuestionType)
            {
                case "TrueFalseQuestion":
                    questionModels.Add(new TrueFalseQuestionModel
                    {
                        id = question.id,
                        question = question.question
                    });
                    break;
                case "MultipleChoiceQuestion":
                    questionModels.Add(new MultipleChoiceQuestion
                    {
                        id = question.id,
                        question = question.question,
                        choices = question.choices
                    });
                    break;
                case "ShortAnswerQuestion":
                    questionModels.Add(new ShortAnswerQuestion
                    {
                        id = question.id,
                        question = question.question
                    });
                    break;
                case "LongAnswerQuestion":
                    questionModels.Add(new LongAnswerQuestion
                    {
                        id = question.id,
                        question = question.question
                    });
                    break;
                default:
                    throw new Exception($"Unknown question type: {question.QuestionType}");
            }
        }
        return questionModels;
    }
}
