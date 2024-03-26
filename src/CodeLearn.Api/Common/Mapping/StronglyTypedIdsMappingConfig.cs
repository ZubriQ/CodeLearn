using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseTopics.ValueObjects;
using CodeLearn.Domain.QuestionChoices.ValueObjects;

namespace CodeLearn.Api.Common.Mapping;

public class StronglyTypedIdsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<QuestionChoiceId, int>()
            .MapWith(src => src.Value);

        config.NewConfig<DataTypeId, int>()
            .MapWith(src => src.Value);

        config.NewConfig<ExerciseTopicId, int>()
            .MapWith(src => src.Value);

        config.NewConfig<ExerciseNoteId, int>()
            .MapWith(src => src.Value);

        config.NewConfig<InputOutputExampleId, int>()
            .MapWith(src => src.Value);

        config.NewConfig<MethodParameterId, int>()
            .MapWith(src => src.Value);

        config.NewConfig<TestCaseId, int>()
           .MapWith(src => src.Value);

        config.NewConfig<TestCaseParameterId, int>()
           .MapWith(src => src.Value);
    }
}