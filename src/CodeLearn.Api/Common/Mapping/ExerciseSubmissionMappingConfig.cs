﻿using CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateMethodCodingExerciseSubmission;
using CodeLearn.Application.ExerciseSubmissions.Question.Commands.CreateQuestionExerciseSubmissions;
using CodeLearn.Contracts.ExerciseSubmissions.MethodCoding;
using CodeLearn.Contracts.ExerciseSubmissions.Question;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.Api.Common.Mapping;

public class ExerciseSubmissionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int TestingSessionId, QuestionExerciseSubmissionsRequest Request),
            CreateQuestionExerciseSubmissionsCommand>()
                .Map(dest => dest.TestingSessionId, src => src.TestingSessionId)
                .Map(dest => dest, src => src.Request);

        config.NewConfig<(int TestingSessionId, MethodCodingExerciseSubmissionRequest Request),
            CreateMethodCodingExerciseSubmissionCommand>()
                .Map(dest => dest.TestingSessionId, src => src.TestingSessionId)
                .Map(dest => dest, src => src.Request);

        config.NewConfig<Result, MethodCodingExerciseSubmissionResponse>()
             .Map(dest => dest.TestingInfoOutput, src => string.IsNullOrEmpty(src.Error.Message)
                 ? "Success"
                 : src.Error.Message);
    }
}