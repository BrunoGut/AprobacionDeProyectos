using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidFilterParameterException : Exception
    {
        public InvalidFilterParameterException(string message) : base(message) { }
    }
}
