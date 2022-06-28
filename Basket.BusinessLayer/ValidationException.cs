using System;

namespace Basket.BusinessLayer
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            :base(message)
        {

        }
    }
}
