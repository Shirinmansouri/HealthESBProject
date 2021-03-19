using HealthESB.Domain.Model;
using HealthESB.Framework.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HealthESB.API.Configuration
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly Action<RequestProfilerModel> _requestResponseHandler;
        private const int ReadChunkBufferLength = 4096;

        readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, Action<RequestProfilerModel> requestResponseHandler,
            ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _requestResponseHandler = requestResponseHandler;

            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token);
             
            // await _next(context);

            var model = new RequestProfilerModel
            {
                RequestTime = new DateTimeOffset(),
                Context = context,
                Request = await FormatRequest(context)
            };

            Stream originalBody = context.Response.Body;

            using (MemoryStream newResponseBody = _recyclableMemoryStreamManager.GetStream())
            {
                context.Response.Body = newResponseBody;

                await _next(context);

                newResponseBody.Seek(0, SeekOrigin.Begin);
                await newResponseBody.CopyToAsync(originalBody);

                newResponseBody.Seek(0, SeekOrigin.Begin);
                model.Response = FormatResponse(context, newResponseBody);
                model.ResponseTime = new DateTimeOffset();
                _requestResponseHandler(model);
            }
        }
        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Constants.TokenKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.Items["UserId"] = jwtToken.Claims.First(x => x.Type == "UserId").Value;
                Constants.LoginUserId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
                

            }
            catch 
            {
                throw ;
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
        private string FormatResponse(HttpContext context, MemoryStream newResponseBody)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string LoggedResponse = $"Http Response Information: " +
                                    $"Host: {request.Host} " +
                                    $"StatusCode: {response.StatusCode} " +
                                    $"Response Body: {ReadStreamInChunks(newResponseBody)} ";

            _logger.LogInformation(LoggedResponse);

            return LoggedResponse;
        }

        private async Task<string> FormatRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            string ClientIpAddress = request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            string LoggedRequest =
                $"Request received from ip address:${ClientIpAddress}\n Http Request Information:" +
                $"Host: {request.Host} " +
                $"Request Body: {await GetRequestBody(request)}";

            _logger.LogInformation(LoggedRequest);

            return LoggedRequest;
        }

        public async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            // request.EnableRewind();
            using (var requestStream = _recyclableMemoryStreamManager.GetStream())
            {
                await request.Body.CopyToAsync(requestStream);
                request.Body.Seek(0, SeekOrigin.Begin);
                return ReadStreamInChunks(requestStream);
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            string result;
            using (var textWriter = new StringWriter())
            using (var reader = new StreamReader(stream))
            {
                var readChunk = new char[ReadChunkBufferLength];
                int readChunkLength;
                //do while: is useful for the last iteration in case readChunkLength < chunkLength
                do
                {
                    readChunkLength = reader.ReadBlock(readChunk, 0, ReadChunkBufferLength);
                    textWriter.Write(readChunk, 0, readChunkLength);
                } while (readChunkLength > 0);

                result = textWriter.ToString();
            }

            return result;
        }
        public int? ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.TokenKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var UserId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

                // return account id from JWT token if validation successful
                return UserId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
        public class RequestProfilerModel
        {
            public DateTimeOffset RequestTime { get; set; }
            public HttpContext Context { get; set; }
            public string Request { get; set; }
            public string Response { get; set; }
            public DateTimeOffset ResponseTime { get; set; }
        }
    }
}
