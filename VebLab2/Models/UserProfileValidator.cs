using FluentValidation;

namespace VebLab2.Models
{
    public class UserProfileValidator : AbstractValidator<UserProfile>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Введіть ім'я");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Введіть прізвище");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Введіть коректний Email");
            RuleFor(x => x.Age).NotNull().InclusiveBetween(18, 100).WithMessage("Вік має бути від 18 до 100 років");
            RuleFor(x => x.BirthDate).NotNull().WithMessage("Вкажіть дату народження");
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\+380\d{9}$").WithMessage("Телефон має бути у форматі +380XXXXXXXXX");
            RuleFor(x => x.ExperienceYears).NotNull().GreaterThanOrEqualTo(0).WithMessage("Досвід не може бути від'ємним");
            RuleFor(x => x.Position).NotEmpty().MinimumLength(3).WithMessage("Посада має містити мінімум 3 символи");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Пароль має містити мінімум 6 символів");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Паролі не співпадають");
        }
    }
}