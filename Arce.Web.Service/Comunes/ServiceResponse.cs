using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Web.Service.Comunes
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public int CodeResult { get; set; }
        public string? Message { get; set; }
        public T? Element { get; set; }
        public int CodeTransacc { get; set; }
    }
}
