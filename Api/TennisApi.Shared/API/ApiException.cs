using System;
using System.Collections.Generic;

namespace TennisApi.Filters.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public List<string> ErrorList { get; set; }

        public ApiException(string message, int statusCode = 500, List<string> errorList = null) : base(message)
        {
            StatusCode = statusCode;
            ErrorList = errorList;
        }
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
