namespace CodeLearn.Application.Common.IdentityModels;

public class StudentUserDetails
{
    public string StudentGroupName { get; init; }
    public string UserCode { get; init; }

    private StudentUserDetails(string studentGroupName, string userCode)
    {
        StudentGroupName = studentGroupName;
        UserCode = userCode;
    }

    public static StudentUserDetails Create(string studentGroupName, string userCode)
    {
        return new(studentGroupName, userCode);
    }
}