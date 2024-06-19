using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Formatters
{
    public class ApiResponse(string code, string status, string message, object? data = null)
    {
        public object Data { get; set; } = data;
        public string Status { get; set; } = status;
        public string StatusCode { get; set; } = code;
        public string Message { get; set; } = message;
    }
}
