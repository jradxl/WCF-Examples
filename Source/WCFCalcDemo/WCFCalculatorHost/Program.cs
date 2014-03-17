
namespace CalculatorHost
{
    // program.cs
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using WCFCalcServiceLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculator Service Demo Server - Version 4\n");

            // Step 1 Create a string address and URI to serve as the base address.
            string strAdr = @"http://localhost:8002/CalculatorService";
            Uri baseAddress = new Uri(strAdr);

            // Step 2 Create a ServiceHost instance
            ServiceHost selfHost = new ServiceHost(typeof(CalculatorService), baseAddress);

            try
            {
                // Step 3 Add a service endpoint.
                //The WSHttpBinding is similar to the BasicHttpBinding but provides more Web service features. 
                //It uses the HTTP transport and provides message security, as does BasicHttpBinding, but it also provides transactions,
                //reliable messaging, and WS-Addressing, either enabled by default or available through a single control setting.

                //BasicHttpBinding httpb = new BasicHttpBinding();
                //OR
                WSHttpBinding wshttp = new WSHttpBinding();
                
                //selfHost.AddServiceEndpoint(typeof(ICalculator), httpb, strAdr);
                //OR
                selfHost.AddServiceEndpoint(typeof(ICalculator), wshttp, baseAddress);

                // Step 4 Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);
                selfHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                // Step 5 Start the service.
                selfHost.Open();

                Console.WriteLine("The service is ready at {0}\n", strAdr);
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred: {0}", ex.Message);
                selfHost.Abort();
            }
            Console.WriteLine("Press <ENTER> to terminate the program.");
            Console.ReadLine();
        }
    }
}
