using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univendas.CException
{
    /// <summary>
    /// Class client of exception 
    /// </summary>
    public class UserException : Exception
    {
        public UserException() { }
        public UserException(string message):base(message)
        {

        }

        public UserException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
