namespace CodeLearn.Application.Users.Commands.ImportStudentList;

public class FileDataDto(string fileName, Stream dataStream, string contentType)
{
    public string FileName { get; } = fileName;
    public Stream DataStream { get; } = dataStream;
    public string ContentType { get; } = contentType;
}