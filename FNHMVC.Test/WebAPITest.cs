using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using FNHMVC.Model;
using FNHMVC.Model.Commands;
using AutoMapper;

namespace FNHMVC.Test
{
    [TestClass()]
    public class WebAPITest
    {
        public TestContext TestContext { get; set; }

        public string URLServer { get; set; }

        public WebAPITest()
        {
            this.URLServer = "http://localhost:1233/";
        }

        [TestMethod()]
        public void CategoryGetTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.URLServer);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = client.GetAsync("api/category/get").Result;
                Assert.IsTrue(response.IsSuccessStatusCode, "Error: Unable to Connect to Server");
                if (response.IsSuccessStatusCode)
                {
                    List<DTOCategory> offers = response.Content.ReadAsAsync<List<DTOCategory>>().Result;
                    Assert.IsTrue(offers != null, "Error: No Category Returned by Server");
                }
            }
        }

        [TestMethod()]
        public void CategoryDetailTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.URLServer);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = client.GetAsync("api/category/get/1").Result;
                Assert.IsTrue(response.IsSuccessStatusCode, "Error: Unable to Connect to Server");
                if (response.IsSuccessStatusCode)
                {
                    DTOCategory category = response.Content.ReadAsAsync<DTOCategory>().Result;
                    Assert.IsTrue(category != null, "Error: No Category Returned by Server");
                }
            }
        }

        
    }
}
