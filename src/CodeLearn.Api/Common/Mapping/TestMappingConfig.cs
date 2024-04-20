using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Application.Tests.Commands.UpdateTest;
using CodeLearn.Application.Tests.Queries.GetTestByIdWithExerciseIds;
using CodeLearn.Application.Tests.Queries.GetTestByIdWithExercises;
using CodeLearn.Contracts.Tests;
using CodeLearn.Domain.Tests;

namespace CodeLearn.Api.Common.Mapping;

public class TestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestRequest, CreateTestCommand>();

        config.NewConfig<Test, TestResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<(int Id, UpdateTestDetailsRequest Request), UpdateTestCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<TestWithExercisesDto, TestWithExercisesResponse>()
            .Map(dest => dest.Id, src => src.Test.Id.Value)
            .Map(dest => dest.Title, src => src.Test.Title)
            .Map(dest => dest.Description, src => src.Test.Description)
            .Map(dest => dest.IsPublic, src => src.Test.IsPublic)
            .Map(dest => dest.Created, src => src.Test.Created)
            .Map(dest => dest.LastModified, src => src.Test.LastModified)
            .Map(dest => dest.MethodCodingExercises, src => src.MethodCodingExercises
                .Select(x => new ExerciseResponseDto(x.Id.Value, x.Title, x.Description, x.Difficulty.ToString()))
                .ToArray())
            .Map(dest => dest.QuestionExercises, src => src.QuestionExercises
                .Select(x => new ExerciseResponseDto(x.Id.Value, x.Title, x.Description, x.Difficulty.ToString()))
                .ToArray());

        config.NewConfig<TestWithExerciseIdsDto, TestWithExerciseIdsResponse>();
    }
}