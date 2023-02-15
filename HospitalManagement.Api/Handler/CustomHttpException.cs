using System;
using System.Runtime.Serialization;

namespace HospitalManagement.Api.Handler
{
    [Serializable]
    public class CustomHttpException : Exception
    {
        public CustomHttpException()
        {
        }

        public CustomHttpException(string? message) : base(message)
        {
        }

        public CustomHttpException(string? message, Exception? innerException, int statusCodes) : base(message, innerException)
        {
        }

        public CustomHttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CustomHttpException(string? message, int statusCodes) : this(message)
        {
        }
    }
}

