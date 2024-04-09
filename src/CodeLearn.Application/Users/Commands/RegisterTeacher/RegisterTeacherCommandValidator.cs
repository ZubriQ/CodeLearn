//namespace CodeLearn.Application.Users.Commands.RegisterTeacher;

//public class RegisterTeacherCommandValidator : AbstractValidator<RegisterTeacherCommand>
//{
//    public RegisterTeacherCommandValidator()
//    {
//        RuleFor(x => x.Credentials)
//            .NotNull();

//        RuleFor(x => x.Credentials.Email)
//            .NotNull()
//            .MaximumLength(250);

//        RuleFor(x => x.Credentials.Password)
//            .NotEmpty().WithMessage("Your password cannot be empty")
//            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
//            .MaximumLength(32).WithMessage("Your password length must not exceed 32.")
//            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
//            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
//            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
//            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

//        RuleFor(x => x.FullName)
//            .NotNull();

//        RuleFor(x => x.FullName.FirstName)
//            .NotNull()
//            .MaximumLength(50);

//        RuleFor(x => x.FullName.FirstName)
//            .NotNull()
//            .MaximumLength(50);

//        RuleFor(x => x.FullName.Patronymic)
//            .MaximumLength(50);
//    }
//}