using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAppLafise.Utilities
{
    public class Utilities
    {

        public Task<HttpResponseMessage> GetsApi(string controller)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7026/");
                //HTTP GET
                var responseTask = client.GetAsync(controller);
                responseTask.Wait();

                return responseTask;
            }


        }
    }
}
