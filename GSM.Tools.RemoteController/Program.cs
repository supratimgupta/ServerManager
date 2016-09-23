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
            //string wcfRegisterCommand = @"netsh http add urlacl url=http://+:"+System.Configuration.ConfigurationManager.AppSettings["BASE_PORT_WCF"]+@" user=DOMAIN\user";

            //try
            //{
            //    string res = ExecuteCommandAsAdmin(wcfRegisterCommand);
            //}
            //catch { }

            try
            {
                string wcfHostAddress = "http://" + System.Configuration.ConfigurationManager.AppSettings["BASE_DOMAIN_WCF"] + ":" + System.Configuration.ConfigurationManager.AppSettings["BASE_PORT_WCF"] + "/Commander.svc";
                Uri baseAddress = new Uri(wcfHostAddress);
                ServiceHost host = new ServiceHost(typeof(RemoteProcSvc));
               
                // Enable metadata publishing.
                //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                //smb.HttpGetEnabled = true;
                //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                //host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                //Console.WriteLine("Press <Enter> to stop the service.");
                //Console.ReadLine();

                // Close the ServiceHost.
                //host.Close();
            }
            catch(Exception exp)
            {
                Console.WriteLine("WCF: HOSTING ERROR - " + exp.Message);
            }
        }

        private static string ExecuteCommandAsAdmin(string command)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo()
                {
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "runas.exe",
                    Arguments = "/user:Administrator \"cmd /K " + command + "\""
                };

                using (Process proc = new Process())
                {
                    proc.StartInfo = procStartInfo;
                    proc.Start();

                    string output = proc.StandardOutput.ReadToEnd();

                    if (string.IsNullOrEmpty(output))
                        output = proc.StandardError.ReadToEnd();

                    return output;
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
                throw exp;
            }
            
        }

        private void RunMonitor()
        {
            ServerDTO serverDTO = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["BASE_ADDRESS_API"]);
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
