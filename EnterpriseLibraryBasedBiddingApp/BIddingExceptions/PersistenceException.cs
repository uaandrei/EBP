using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiddingExceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string persistenceMessage, string exceptionMessage, Exception innerException)
            : base(exceptionMessage, innerException)
        {
            this.PersistenceMessage = persistenceMessage;
        }

        public string PersistenceMessage { get; private set; }
    }
}
