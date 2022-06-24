using CulinaryApi.Core.Entieties;
using CulinaryApi.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;

namespace CulinaryApi.Infrastructure.Extensions
{
    public class UserServiceExtension
    {
        public static void UserVerification(User user)
        {
            if (user == null)
            {
                throw new NotFoundException($"User {user} is not found");
            }
        }

        public static void PasswordVerification(PasswordVerificationResult result)
        {
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user name or password");
            }
        }
    }
}
