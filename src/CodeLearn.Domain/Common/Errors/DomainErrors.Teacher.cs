﻿namespace CodeLearn.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class Teacher
    {
        private static string Prefix => "Teacher.";

        public static readonly Error MaxPatronymicLengthExceeded = new(
            $"{Prefix}{nameof(MaxPatronymicLengthExceeded)}",
            "Patronymic can't be longer than 50 characters.");

        public static readonly Error InvalidFirstName = new(
            $"{Prefix}{nameof(InvalidFirstName)}",
            "First name can't be longer than 50 characters or be empty.");

        public static readonly Error InvalidLastName = new(
            $"{Prefix}{nameof(InvalidLastName)}",
            "Last name can't be longer than 50 characters or be empty.");
    }
}