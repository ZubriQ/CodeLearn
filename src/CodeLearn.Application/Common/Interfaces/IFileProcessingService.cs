using CodeLearn.Application.Users.Commands.ImportStudentList;

namespace CodeLearn.Application.Common.Interfaces;

public interface IFileProcessingService
{
    /// <summary>
    /// Get an excel files and creates dto's based on rows and student group name.
    /// </summary>
    /// <param name="dataStream">Excel File.</param>
    /// <param name="studentGroupName">Associated student group name with the excel file.</param>
    /// <returns>Student dtos.</returns>
    Task<ImportedStudentDto[]> CreateStudentDtosFromExcel(Stream dataStream, string studentGroupName);
}