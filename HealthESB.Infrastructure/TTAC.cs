using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Framework.Utility;
using HealthESB.Infrastructure.Model;
using HealthESB.RabbitMQ.IContract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthESB.Infrastructure
{
    public class TTAC
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IServiceProvider _serviceProvider;
        public TTAC(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            _rabbitMqService = (IRabbitMqService)this._serviceProvider.GetRequiredService(typeof(IRabbitMqService));
        }
        public async Task<TTACPrescriptionResponse> CallPrescriptionApi(TTACPrescriptionRequest prescriptionRequest)
        {
            var res = new TTACPrescriptionResponse();
            var request = (HttpWebRequest)WebRequest.Create(Constants.TTAC_BaseUrl + Constants.TTAC_RegisterApiName);
            request.Method = "POST";
            //request.Headers.Add("X-SSP-Api-Key", "56c671ff-4cdf-4907-8e7d-3158fa7dec3e");
            request.Headers.Add("X-SSP-Api-Key", Constants.TTAC_RegisterApiKey);
            request.ContentType = @"application/json";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(prescriptionRequest));
                }
                var httpResponse = (HttpWebResponse)(await request.GetResponseAsync().ConfigureAwait(false));
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    JObject jObject = JObject.Parse(result);
                    res.ErrorCode = int.Parse(jObject["ErrorCode"]?.ToString() ?? "-1");
                    res.ErrorMessage = jObject["ErrorMessage"]?.ToString() ?? "";
                    res.PrescriptionId = int.Parse(jObject["PrescriptionId"]?.ToString() ?? "0");
                }
            }
            catch (WebException ex)
            {
                res.ErrorCode = (int)HealthESBApiResponseCode.ServiceProviderNotResponse;
                res.ErrorMessage = HealthESBApiResponseMessages.ServiceProviderNotResponse;
            }

            return res;

        }
        public async Task<TTACPrescriptionBarcodeResponse> CallCheckSingleBarcodeApi(TTACPrescriptionBarcodeRequest tTACPrescriptionBarcodeRequest)
        {
            var res = new TTACPrescriptionBarcodeResponse();
            var request = (HttpWebRequest)WebRequest.Create(Constants.TTAC_BaseUrl + Constants.TTAC_CheckApiName);
            request.Method = "POST";
            request.Headers.Add("X-SSP-Api-Key", Constants.TTAC_CheckApiKey);
            request.ContentType = @"application/json";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(tTACPrescriptionBarcodeRequest));
                }
                var httpResponse = (HttpWebResponse)(await request.GetResponseAsync().ConfigureAwait(false));
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    JObject jObject = JObject.Parse(result);
                    res.ErrorCode = int.Parse(jObject["ErrorCode"]?.ToString() ?? "-1");
                    res.ErrorMessage = jObject["ErrorMessage"]?.ToString() ?? "";
                    res.PrescriptionId = int.Parse(jObject["PrescriptionId"]?.ToString() ?? "0");
                    if (jObject["ItemsInfo"].ToString() != "[]")
                    {
                        res.ItemsInfo = new List<TTACPrescriptionBarcodeDetailesResponse>();
                        JToken jInfo = jObject["ItemsInfo"];
                        for (int i = 0; i < jInfo.Count(); i++)
                        {

                            TTACPrescriptionBarcodeDetailesResponse tTACPrescriptionBarcodeDetailesResponse = new TTACPrescriptionBarcodeDetailesResponse();
                            tTACPrescriptionBarcodeDetailesResponse.EnglishName = jInfo[i]["EnglishName"] == null ? "" : jInfo[0]["EnglishName"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.GenericCode = jInfo[i]["GenericCode"] == null ? "" : jInfo[0]["GenericCode"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Irc = jInfo[i]["Irc"] == null ? "" : jInfo[0]["Irc"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Expiration = jInfo[i]["Expiration"] == null ? "" : jInfo[0]["Expiration"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Status = int.Parse(jInfo[i]["Status"] == null ? "-1" : jInfo[0]["Status"]?.ToString());
                            tTACPrescriptionBarcodeDetailesResponse.StatusMessage = jInfo[i]["StatusMessage"] == null ? "" : jInfo[0]["StatusMessage"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.ProductType = jInfo[i]["ProductType"] == null ? "" : jInfo[0]["ProductType"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.ProductTypeId = int.Parse(jInfo[i]["ProductTypeId"] == null ? "0" : jInfo[0]["ProductTypeId"].ToString());
                            tTACPrescriptionBarcodeDetailesResponse.Price = int.Parse(jInfo[i]["Price"] == null ? "0" : jInfo[0]["Price"].ToString());
                            tTACPrescriptionBarcodeDetailesResponse.TrackingCode = int.Parse(jInfo[i]["TrackingCode"] == null ? "0" : jInfo[0]["TrackingCode"].ToString());
                            tTACPrescriptionBarcodeDetailesResponse.BarcodeUid = jInfo[0]["BarcodeUid"] == null ? "" : jInfo[0]["BarcodeUid"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Uid = jInfo[0]["Uid"] == null ? "" : jInfo[0]["BarcodeUid"].ToString();
                            res.ItemsInfo.Add(tTACPrescriptionBarcodeDetailesResponse);
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                res.ErrorCode = (int)HealthESBApiResponseCode.ServiceProviderNotResponse;
                res.ErrorMessage = HealthESBApiResponseMessages.ServiceProviderNotResponse;
            }

            return res;

        }
        public async Task<TTACReactiveResponse> CallReactiveApi(TTACReactiveRequest tTACReactiveRequest)
        {
            var res = new TTACReactiveResponse();
            var request = (HttpWebRequest)WebRequest.Create(Constants.TTAC_BaseUrl + Constants.TTAC_ReactiveApiName);
            request.Method = "POST";
            request.Headers.Add("X-SSP-Api-Key", Constants.TTAC_ReactiveApiKey);
            request.ContentType = @"application/json";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(tTACReactiveRequest));
                }
                var httpResponse = (HttpWebResponse)(await request.GetResponseAsync().ConfigureAwait(false));
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    JObject jObject = JObject.Parse(result);
                    res.ErrorCode = int.Parse(jObject["ErrorCode"]?.ToString() ?? "-1");
                    res.ErrorMessage = jObject["ErrorMessage"]?.ToString() ?? "";
                    res.PrescriptionId = int.Parse(jObject["PrescriptionId"]?.ToString() ?? "0");
                    if (jObject["ItemsInfo"].ToString() != "[]")
                    {
                        res.ItemsInfo = new List<TTACPrescriptionBarcodeDetailesResponse>();
                        JToken jInfo = jObject["ItemsInfo"];
                        for (int i = 0; i < jInfo.Count(); i++)
                        {

                            TTACPrescriptionBarcodeDetailesResponse tTACPrescriptionBarcodeDetailesResponse = new TTACPrescriptionBarcodeDetailesResponse();
                            tTACPrescriptionBarcodeDetailesResponse.EnglishName = jInfo[i]["EnglishName"] == null ? "" : jInfo[0]["EnglishName"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.GenericCode = jInfo[i]["GenericCode"] == null ? "" : jInfo[0]["GenericCode"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Irc = jInfo[i]["Irc"] == null ? "" : jInfo[0]["Irc"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Expiration = jInfo[i]["Expiration"] == null ? "" : jInfo[0]["Expiration"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Status = int.Parse(jInfo[i]["Status"] == null ? "-1" : jInfo[0]["Status"]?.ToString());
                            tTACPrescriptionBarcodeDetailesResponse.StatusMessage = jInfo[i]["StatusMessage"] == null ? "" : jInfo[0]["StatusMessage"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.ProductType = jInfo[i]["ProductType"] == null ? "" : jInfo[0]["ProductType"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.ProductTypeId = int.Parse(jInfo[i]["ProductTypeId"] == null ? "0" : jInfo[0]["ProductTypeId"].ToString());
                            tTACPrescriptionBarcodeDetailesResponse.Price = int.Parse(jInfo[i]["Price"] == null ? "0" : jInfo[0]["Price"].ToString());
                            tTACPrescriptionBarcodeDetailesResponse.TrackingCode = int.Parse(jInfo[i]["TrackingCode"] == null ? "0" : jInfo[0]["TrackingCode"].ToString());
                            tTACPrescriptionBarcodeDetailesResponse.BarcodeUid = jInfo[0]["BarcodeUid"] == null ? "" : jInfo[0]["BarcodeUid"].ToString();
                            tTACPrescriptionBarcodeDetailesResponse.Uid = jInfo[0]["Uid"] == null ? "" : jInfo[0]["BarcodeUid"].ToString();
                            res.ItemsInfo.Add(tTACPrescriptionBarcodeDetailesResponse);
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                res.ErrorCode = (int)HealthESBApiResponseCode.ServiceProviderNotResponse;
                res.ErrorMessage = HealthESBApiResponseMessages.ServiceProviderNotResponse;
            }

            return res;

        }
    }
}
