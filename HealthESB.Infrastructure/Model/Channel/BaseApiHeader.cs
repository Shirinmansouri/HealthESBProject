using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model.Channel
{
    public  class BaseApiHeader
    {
        public  Method ApiMethodType { get; set; } = Method.POST;
        public string ContentType { get; set; }
        public string ApiUrl { get; set; }
        public BaseApiHeader(string apiUrl, int apiMethodType = 1, string contentType = "application/json")
        {

            ApiUrl = apiUrl;
            ApiMethodType = (Method)apiMethodType;
            ContentType = contentType;
        }
    }
}
