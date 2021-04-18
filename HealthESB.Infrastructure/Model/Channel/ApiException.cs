using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model.Channel
{ 
    [Serializable]
    public class ApiException : Exception
    {
        private string[] messages;
        public string[] Messages => messages;
        public ApiException()
        {
        }
        public ApiException(string[] messages) : base(messages.Length >= 1 ? messages[0] : "")
        {
            this.messages = messages;

        }
        public ApiException(string messages) : base(messages)
        {
            this.messages = new[] { messages };
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
