using System;
using System.Collections.Generic;
using System.Text;

namespace TennisApi.Shared.API
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public string Detail { get; set; }

        public object Data { get; set; }
        public List<string> ErrorList { get; set; }

        public ApiResponse(object data)
        {
            this.Data = data;
            this.Error = false;
            this.ErrorList = new List<string>();
        }
        public ApiResponse(string message, bool error)
        {
            this.Message = message;
            this.Error = error;
            this.ErrorList = new List<string>();
        }
        public ApiResponse(string message, object data, bool error)
        {
            this.Message = message;
            this.Error = error;
            this.Data = data;
            this.ErrorList = new List<string>();
        }
    }

    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public string Detail { get; set; }

        public T Data { get; set; }
        public List<string> ErrorList { get; set; }

        public ApiResponse()
        {
            this.ErrorList = new List<string>();
        }
        public ApiResponse(T data)
        {
            this.Data = data;
            this.ErrorList = new List<string>();
        }
        public ApiResponse(string message, bool error)
        {
            this.Message = message;
            this.Error = error;
            this.ErrorList = new List<string>();
        }
        public ApiResponse (string message, T data, bool error)
        {
            this.Message = message;
            this.Error = error;
            this.Data = data;
            this.ErrorList = new List<string>();
        }
    }
}
