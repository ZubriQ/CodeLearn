using CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;
using CodeLearn.Contracts.QuestionExercises;

namespace CodeLearn.Api.Common.Mapping;

public class QuestionExerciseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int Id, QuestionExerciseRequest Request), CreateQuestionExerciseCommand>()
            .Map(dest => dest.TestId, src => src.Id)
            .Map(dest => dest, src => src.Request);

        //config.NewConfig<(int Id, StudentGroupRequest Request), UpdateStudentGroupCommand>()
        //    .Map(dest => dest.Id, src => src.Id)
        //    .Map(dest => dest, src => src.Request);
    }
}
