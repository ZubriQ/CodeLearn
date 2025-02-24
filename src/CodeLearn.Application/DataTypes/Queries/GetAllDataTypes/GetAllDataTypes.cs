using CodeLearn.Domain.Exercises.Entities;

namespace CodeLearn.Application.DataTypes.Queries.GetAllDataTypes;

public record GetAllDataTypesQuery : IRequest<DataType[]>;

public class GetAllDataTypesQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllDataTypesQuery, DataType[]>
{
    public async Task<DataType[]> Handle(GetAllDataTypesQuery request, CancellationToken cancellationToken)
    {
        var dataTypes = await context.DataTypes
            .AsNoTracking()
            .Where(x => x.Alias != "void")
            .ToArrayAsync(cancellationToken);

        return dataTypes.Length == 0 ? [] : dataTypes;
    }
}