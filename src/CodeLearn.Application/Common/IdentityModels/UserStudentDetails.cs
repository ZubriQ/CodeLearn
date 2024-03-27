namespace CodeLearn.Application.Common.IdentityModels;

public class UserStudentDetails
{
    public string? StudentGroupName { get; init; }
    public int? EnrolmentYear { get; init; }

    private UserStudentDetails(string studentGroupName, int enrolmentYear)
    {
        StudentGroupName = studentGroupName;
        EnrolmentYear = enrolmentYear;
    }

    public static UserStudentDetails Create(string studentGroupName, int enrolmentYear)
    {
        return new(studentGroupName, enrolmentYear);
    }
}