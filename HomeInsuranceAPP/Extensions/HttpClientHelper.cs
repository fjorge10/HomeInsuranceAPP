using HomeInsuranceAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeInsuranceAPP.Extensions
{
    public static class HttpClientHelper
    {
        public static HttpClient HttpClientInstance(string clientURL)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(clientURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}