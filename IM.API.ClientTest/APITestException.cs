using System;

namespace IM.API.ClientTest
{
    public class APITestException : Exception
    {
        public APITestException(string message)
            :base(message)
        {
            
        }
    }
}
