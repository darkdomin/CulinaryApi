using System;

namespace CulinaryApi.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException(string message) : base (message)
        {

        }
    }
}
