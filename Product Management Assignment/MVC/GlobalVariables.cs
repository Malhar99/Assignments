using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MVC
{
    public static class GlobalVariables
    {
        public static HttpClient webApiclient = new HttpClient();

        static GlobalVariables()
        {
            webApiclient.BaseAddress = new Uri("http://localhost:59368/api/");
            webApiclient.DefaultRequestHeaders.Clear();
            webApiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}