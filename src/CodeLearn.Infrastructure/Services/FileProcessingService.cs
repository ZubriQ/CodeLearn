using ClosedXML.Excel;
using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Application.Users.Commands.ImportStudentList;

namespace CodeLearn.Infrastructure.Services;

public class FileProcessingService : IFileProcessingService
{
    public async Task<ImportedStudentDto[]> CreateStudentDtosFromExcel(Stream dataStream, string studentGroup)
    {
        var studentsToCreate = new List<ImportedStudentDto>();

        GetStudentsFromWorkbook(dataStream, studentsToCreate, studentGroup);

        return studentsToCreate.Count == 0 ? [] : [.. studentsToCreate];
    }

    private static void GetStudentsFromWorkbook(
        Stream dataStream, List<ImportedStudentDto> studentsToCreate, string studentGroup)
    {
        using var workbook = new XLWorkbook(dataStream);

        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

        foreach (var row in rows)
        {
            var lastName = row.Cell(2).GetValue<string>();
            var firstName = row.Cell(3).GetValue<string>();
            var patronymic = row.Cell(4).GetValue<string>();
            var userCode = row.Cell(5).GetValue<string>();

            var userDto = new ImportedStudentDto
            {
                LastName = lastName.Trim(),
                FirstName = firstName.Trim(),
                Patronymic = patronymic.Trim(),
                StudentGroup = studentGroup.Trim(),
                UserCode = userCode.Trim(),
            };

            studentsToCreate.Add(userDto);
        }
    }
}