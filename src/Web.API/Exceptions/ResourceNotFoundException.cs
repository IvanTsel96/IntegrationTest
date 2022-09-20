using System;

namespace Web.API.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}
