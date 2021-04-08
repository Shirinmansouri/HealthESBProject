using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using HealthESB.Framework.Utility;
using System.Net;
using HealthESB.Domain.Model;
using HealthESB.Domain.IService;

namespace HealthESB.API.Controllers
{
    public class UserAccess : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var _userService = (IAspNetUserRolesService)filterContext.HttpContext.RequestServices.GetService(typeof(IAspNetUserRolesService));
            var request = filterContext.HttpContext.Request;
            var route = request.Path.HasValue ? request.Path.Value : "";
            var requestHeader = request.Headers.Aggregate("", (current, header) => current + $"{header.Key}: {header.Value}{Environment.NewLine}");
            string requestBody = "";
            //request.EnableRewind();
            if (request.Method == "GET")
            {
                Microsoft.AspNetCore.Http.QueryString queryString = request.QueryString;
                var dict = HttpUtility.ParseQueryString(queryString.ToString());
                string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
                requestBody = json;
            }
            else if (request.Method == "POST")
            {
                using (var stream = new StreamReader(request.Body))
                {
                    stream.BaseStream.Position = 0;
                    requestBody = stream.ReadToEnd();
                }
            }

            if ((route.ToLower() == "/api/AuthManagement/Login") || (route.ToLower() == "/api/AuthManagement/GetUserClaims".ToLower()))
                base.OnActionExecuting(filterContext);
            else
            {
                Boolean IsAuthorized = false;
                Task<ClaimsResponse> claimsResponse = _userService.getUserClaimsByUserIdAsync(filterContext.HttpContext.Items["UserId"].ToString());
                foreach (var item in claimsResponse.Result.Claims)
                {
                    if (route.ToLower().Equals(("/api/" + item.ControlleEnTitile.Trim() + "/" + item.ActionTitleEn.Trim()).ToLower()))
                    {
                        IsAuthorized = true;
                        break;
                    }

                }
                if(IsAuthorized)
                base.OnActionExecuting(filterContext);
                else
                filterContext.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
            }

        }


    }
}
