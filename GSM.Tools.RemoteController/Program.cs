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
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Diagnostics;

namespace GSM.Tools.RemoteController
{
    class Program
    {
        private IServerSvc _serverSvc = new GSM.Service.Implementations.ServerSvc();
        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.HostRemoteController();
            pr.RunMonitor();
        }

        private void HostRemoteController()
        {
            try
            {
                string wcfHostAddress = System.Configuration.ConfigurationManager.AppSettings["BASE_ADDR_WCF"];
                Uri baseAddress = new Uri(wcfHostAddress);
                ServiceHost host = new ServiceHost(typeof(RemoteProcSvc));
               
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
            }
            catch(Exception exp)
            {
                Console.WriteLine("WCF: HOSTING ERROR - " + exp.Message);
            }
        }

        private void RunMonitor()
        {
            ServerDTO serverDTO = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["BASE_ADDRESS_API"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IServerSvc _iSvc = null;
            string wcfHostAddress = System.Configuration.ConfigurationManager.AppSettings["BASE_ADDR_WCF"];
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                using(_iSvc = new ServerSvc())
                {
                    serverDTO = _iSvc.GetServerInformation();
                    serverDTO.WCFHostingURL = wcfHostAddress;
                    if(!string.IsNullOrEmpty(serverDTO.ServerIP) && !string.IsNullOrEmpty(serverDTO.ServerName))
                    {
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
                    else
                    {
                        Console.WriteLine("Server IP or Server Name is not found");
                    }
                }
            }
        }
    }
}
