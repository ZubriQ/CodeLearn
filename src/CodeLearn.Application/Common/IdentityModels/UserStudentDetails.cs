namespace CodeLearn.Application.Common.IdentityModels;

public class UserStudentDetails
{
    public string StudentGroupName { get; init; }
    public string UserCode { get; init; }

    private UserStudentDetails(string studentGroupName, string userCode)
    {
        StudentGroupName = studentGroupName;
        UserCode = userCode;
    }

    public static UserStudentDetails Create(string studentGroupName, string userCode)
    {
        return new(studentGroupName, userCode);
    }
}