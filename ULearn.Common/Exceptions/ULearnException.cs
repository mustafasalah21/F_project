using System;

namespace ULearn.Common.Extensions
{
    public class ULearnException : Exception
    {
        public int ErrorCode { get; set; }

        public ULearnException() : base("ULearn Exception")
        {
        }

        public ULearnException(string message) : base(message)
        {
        }

        public ULearnException(int statusCode, string message) : base(message)
        {
            ErrorCode = statusCode;
        }

        public ULearnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ULearnException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = statusCode;
        }
    }
}
