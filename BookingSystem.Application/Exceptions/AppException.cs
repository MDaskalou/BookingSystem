using System;

namespace BookingSystem.Application.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }
    }
}