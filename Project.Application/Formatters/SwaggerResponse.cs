using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Formatters
{
    public class SwaggerResponse<T> where T : class
    {
        public T Data { get; set; } = null!;
        public string StatusCode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
