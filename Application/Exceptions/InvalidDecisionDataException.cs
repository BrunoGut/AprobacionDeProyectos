using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidDecisionDataException : Exception
    {
        public InvalidDecisionDataException(string message) : base(message) { }
    }
}
