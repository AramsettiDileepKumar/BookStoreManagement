using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Exceptions
{
    public class UserNotFoundException:Exception
    {
        public UserNotFoundException(string v)
        {
        }

        public UserNotFoundException(string? message, string v) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
