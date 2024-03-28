using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Queries.GetTestByIdWithExercises;

public record TestWithExercisesDto(
    Test Test,
    MethodCodingExercise[] MethodCodingExercises,
    QuestionExercise[] QuestionExercises);