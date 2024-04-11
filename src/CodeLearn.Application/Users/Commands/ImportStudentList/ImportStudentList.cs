namespace CodeLearn.Application.Users.Commands.ImportStudentList;

public record ImportStudentListCommand(FileDataDto File, string StudentGroup) : IRequest<OneOf<Success, BadRequest, NotFound>>;

public class ImportStudentListCommandHandler(
    IFileProcessingService _fileService,
    IIdentityService _identityService,
    IApplicationDbContext _context)
    : IRequestHandler<ImportStudentListCommand, OneOf<Success, BadRequest, NotFound>>
{
    public async Task<OneOf<Success, BadRequest, NotFound>> Handle(ImportStudentListCommand request, CancellationToken cancellationToken)
    {
        if (!IsFileFormatExcel(request.File.ContentType) || string.IsNullOrEmpty(request.StudentGroup))
        {
            return new BadRequest();
        }

        var studentGroupExists = await _context.StudentGroups
            .AnyAsync(x => x.Name == request.StudentGroup, cancellationToken);

        if (!studentGroupExists)
        {
            return new NotFound();
        }

        var importedStudentDtos = await _fileService.
            CreateStudentDtosFromExcel(request.File.DataStream, request.StudentGroup);

        if (importedStudentDtos.Length == 0)
        {
            return new BadRequest();
        }

        var result = await _identityService
            .AddStudentUsersFromDtoAsync(importedStudentDtos, request.StudentGroup);

        if (result.IsFailure)
        {
            return new BadRequest();
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Success();
    }

    private static bool IsFileFormatExcel(string contentType)
    {
        return !string.IsNullOrEmpty(contentType) && contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}