using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntTrackerLibrary.Helper
{
    class JobHuntTrackerAPIHelper
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://jobhuntapi.azurewebsites.net/");
            return Client;
        }
    }
}
