namespace CodeLearn.CodeTester.Models;

public class CodeExercise
{
    public string ClassName { get; set; } = string.Empty;
    
    public string StudentCode { get; set; } = string.Empty;
    public string MethodToExecute { get; set; } = string.Empty;
    public string MethodReturnTypeSystemName { get; set; } = string.Empty;
    public List<MethodParameter> MethodParameters { get; set; } = [];

    public List<TestCase> TestCases { get; set; } = [];
}