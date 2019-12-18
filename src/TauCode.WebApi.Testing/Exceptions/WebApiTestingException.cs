using System;

namespace TauCode.WebApi.Testing.Exceptions
{
    [Serializable]
    public class WebApiTestingException : Exception
    {
        public WebApiTestingException()
        {
        }

        public WebApiTestingException(string message) 
            : base(message)
        {
        }

        public WebApiTestingException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }
}
