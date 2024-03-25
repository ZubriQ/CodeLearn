using CodeLearn.Application.Exercises.Commands.CreateMethodCodingExercise;
using CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;
using CodeLearn.Contracts.Exercises.MethodCoding;
using CodeLearn.Contracts.Exercises.Question;

namespace CodeLearn.Api.Common.Mapping;

public class ExerciseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int Id, QuestionExerciseRequest Request), CreateQuestionExerciseCommand>()
            .Map(dest => dest.TestId, src => src.Id)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(int Id, MethodCodingExerciseRequest Request), CreateMethodCodingExerciseCommand>()
            .Map(dest => dest.TestId, src => src.Id)
            .Map(dest => dest, src => src.Request);
    }
}
