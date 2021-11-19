using System;

namespace CulinaryApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base (message)
        {

        }
    }
}
