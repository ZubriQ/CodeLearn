using CodeLearn.Application.Exercises.QuestionExercises.Commands.CreateQuestionExercise;
using CodeLearn.Contracts.Exercises.Question;
using CodeLearn.Domain.Exercises;

namespace CodeLearn.Api.Common.Mapping;

public class QuestionExerciseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int Id, QuestionExerciseRequest Request), CreateQuestionExerciseCommand>()
            .Map(dest => dest.TestId, src => src.Id)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<QuestionExercise, TeacherQuestionExerciseResponse>()
            .Map(dest => dest.TestId, src => src.TestId.Value)
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<QuestionExercise, StudentQuestionExerciseResponse>()
            .Map(dest => dest.TestId, src => src.TestId.Value)
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}