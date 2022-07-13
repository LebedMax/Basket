using System;

namespace Basket.DataAccess;

public class DataServiceException : Exception
{
    public DataServiceException(string message)
        : base(message)
    {
    }
}