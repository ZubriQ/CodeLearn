﻿namespace CodeLearn.Contracts.Users;

public record RegisterTeacherRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string Email,
    string Password);