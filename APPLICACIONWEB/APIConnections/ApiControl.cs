using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APPLICACIONWEB.APIConnections
{
    public class ApiControl
    {
        //string baseUrl = "https://localhost:44324/";
        string baseUrl = "https://webapi20210607135135.azurewebsites.net/";

       
        public async Task<HttpResponseMessage> PostRequest(StringContent data, string urlApi)
        {

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseUrl);

                    client.DefaultRequestHeaders.Clear();


                    HttpResponseMessage response = await client.PostAsync(urlApi, data);

                    return response;
                }
            }
            catch (Exception e) { throw e; }
        }


        public async Task<HttpResponseMessage> GetRequest(string urlApi)
        {

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseUrl);

                    client.DefaultRequestHeaders.Clear();
                   
                    HttpResponseMessage response = await client.GetAsync(urlApi);


                    return response;
                }
            }
            catch (Exception e) { throw e; }
        }


    }

}
