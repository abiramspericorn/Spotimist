using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Models
{
    public class JsonResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public string ExceptionMessage { get; set; }
        public string ValidationMessage { get; set; }

        public T Data { get; set; }

    }
    public enum ResponseStatus
    {
        Success = 1,
        Fail = 2,
        ValidationError = 3,
        Exception = 4
    }
}
