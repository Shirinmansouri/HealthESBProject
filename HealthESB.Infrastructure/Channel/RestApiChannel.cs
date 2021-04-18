using HealthESB.Infrastructure.IChannel;
using HealthESB.Infrastructure.Model.Channel;
using log4net;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Channel
{
    internal class RestApiChannel : IRestApiChannel

    {

        private static readonly ILog _log = LogManager.GetLogger(nameof(RestApiChannel));
        public IRestResponse CallWebApi<THeader, TRequest>(THeader header, TRequest Request) where THeader : BaseApiHeader
            where TRequest : BaseApiRequest
        {
            var client = new RestClient(header.ApiUrl);
            var request = new RestRequest(header.ApiMethodType);
            request.AddHeader("cache-control", "no-cache");
            Type requestObjectType = header.GetType();
            PropertyInfo[] properties = requestObjectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "ApiMethodType" && property.Name != "ApiUrl")
                    request.AddHeader(property.Name, property.GetValue(header, null).ToString());
            }
            request.AddParameter("application/json", Request.ToJson(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }
        public static IRestApiChannel GetChannel() => new RestApiChannel();


    }
}
