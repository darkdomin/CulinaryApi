using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Users;
using FluentValidation;
using System.Linq;

namespace CulinaryApi.Infrastructure.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly CulinaryDbContext _dbContext;
        public RegisterUserDtoValidator(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Email)
                .NotEmpty()
                .MinimumLength(6)
               // .Must(UniqueName)
               .Custom((value, context) =>
               {
                   var emailInUse = dbContext.Users.Any(u => u.Email == value);
                   if (emailInUse)
                   {
                       context.AddFailure("Email", $"This login name - {value} already exists.");
                   }
               });
            // .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }

        private bool UniqueName(string name)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email.ToLower() == name.ToLower());

            if (user == null)
                return true;

            return false;
        }
    }
}
