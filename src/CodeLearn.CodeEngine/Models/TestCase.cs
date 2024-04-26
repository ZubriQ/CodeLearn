namespace CodeLearn.CodeEngine.Models;

public class TestCase
{
    public string CorrectOutputValue { get; set; } = string.Empty;
    public List<TestCaseParameter> TestCaseParameters { get; set; } = [];
}