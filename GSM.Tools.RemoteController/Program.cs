using GSM.Common.Contracts;
using GSM.Common.DTOs;
using GSM.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GSM.Tools.RemoteController
{
    class Program
    {
        private IServerSvc _serverSvc = new GSM.Service.Implementations.ServerSvc();
        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.RunMonitor();
        }

        private void RunMonitor()
        {
            ServerDTO serverDTO = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["BASE_ADDRESS"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IServerSvc _iSvc = null;
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                using(_iSvc = new ServerSvc())
                {
                    serverDTO = _iSvc.GetServerInformation();
                    var response = client.PostAsJsonAsync("api/server/sendDetail", serverDTO).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Call status successful...");
                    }
                    else
                    {
                        Console.WriteLine("Call status error...");
                    }
                }
            }
        }
    }
}
