using System;

namespace Common
{
    public class UserInputError : Exception
    {
        public UserInputError(string message) : base(message)
        {
        }
    }
}