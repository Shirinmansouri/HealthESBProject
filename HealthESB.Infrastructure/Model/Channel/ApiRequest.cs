using HealthESB.Infrastructure.IChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model.Channel
{
    internal class ApiRequest 
    {
        internal string JsonBody { get; set; }
        internal string BaseUri { get; set; }
        internal ApiRequest(string jsonBody,string baseUri)
        {
            JsonBody = jsonBody;
            BaseUri = baseUri;
        }
        //public (bool State, string Message, string FieldName) IsValid()
        //{
        //    if (string.IsNullOrEmpty(JsonBody))
        //        return (false, "value is null or empty", nameof(JsonBody));
        //    return (true, "", "");
        //}
    }
}
