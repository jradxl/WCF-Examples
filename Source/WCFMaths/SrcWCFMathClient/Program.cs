using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ConWCFMathClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() !=6 )
            {
                Console.WriteLine("Insufficient arguments supplied");
                Console.WriteLine("Service Host must be started as ConWCFMathClient <Host Machine> <TCP/HTTP> <Port#> <ADD/SUB/MUL/DIV> Num1 Num2");
                Console.WriteLine("Argument 1 >> IP address or the Machine name, where Service is hosted/running (localhost if running onm the same machine)");
                Console.WriteLine("Argument 2 >> Specifying the Binding type, which binding is used to Host the service. (Supported values are either HTTP or TCP),  without quotes");
                Console.WriteLine("Argument 3 >> Port Number a numeric value");
                Console.WriteLine("Argument 4 >> Operation permissible values are ADD/SUB/MUL/DIV without quotes");
                Console.WriteLine("Argument 5 >> Operand 1 for the operation ");
                Console.WriteLine("Argument 6 >> Operand 2 for the operation ");
                Console.WriteLine("\nExamples");
                Console.WriteLine("<Executable Name> 192.168.1.1 TCP 9001 ADD 1000 2000");  
                Console.WriteLine("<Executable Name> 192.168.1.1 TCP 8001 SUB 500 1000");
                Console.WriteLine("<Executable Name> 192.168.1.1 HTTP 8001 MUL 500 1000");
                Console.WriteLine("<Executable Name> 192.168.1.1 HTTP 9001 DIV 3000 1000");
                Console.WriteLine("<Executable Name> localhost TCP 9001 ADD 4000.0  500.0");
                Console.WriteLine("<Executable Name> localhost TCP 8001 SUB 600.0 700.0");
                Console.WriteLine("<Executable Name> localhost HTTP 8001 MUL 1200.0 300.0");
                Console.WriteLine("<Executable Name> localhost HTTP 9001 DIV 2000.0 90.0");

                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey();
                return;
            }

            string strAdr = args[0];
            string strBinding = args[1].ToUpper();
            bool bSuccess = ((strBinding == "TCP") || (strBinding == "HTTP"));
            if (bSuccess == false)
            {
                Console.WriteLine("\nBinding argument is invalid, should be either TCP or HTTP)");
                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey();
                return;
            }
            
            int nPort = 0;
            bSuccess = int.TryParse(args[2], out nPort);
            if (bSuccess == false)
            {
                Console.WriteLine("\nPort number must be a numeric value");
                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey();
                return;
            }

            string strOper = args[3].ToUpper();
            bSuccess = ((strOper == "ADD") || (strOper == "SUB") || (strOper == "MUL") || (strOper == "DIV"));
            if (bSuccess == false)
            {
                Console.WriteLine("\nOperation argument is invalid, should be ADD/SUB/MUL/DIV)");
                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey();
                return;
            }

            //  Determine operand 1
            double dblNum1 = 0;
            bSuccess = double.TryParse(args[4], out dblNum1);
            if (bSuccess == false)
            {
                Console.WriteLine("\nOperand 1 must be a numeric value");
                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey();
                return;
            }

            //  Determine operand 2
            double dblNum2 = 0;
            bSuccess = double.TryParse(args[5], out dblNum2);
            if (bSuccess == false)
            {
                Console.WriteLine("\nnOperand 2 must be a numeric value");
                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey();
                return;
            }

            Evaluate(strAdr, strBinding, nPort, strOper, dblNum1, dblNum2);

            Console.WriteLine("Press any key to Quit.");
            Console.ReadKey();

        }

        private static void Evaluate(string strServer, string strBinding, int nPort, string strOper, double dblVal1, double dblVal2)
        {
            ChannelFactory<IMathService> channelFactory = null; 
            EndpointAddress ep = null;

            string strEPAdr = "http://" + strServer + ":" + nPort.ToString() + "/MathService";
            try
            {
                switch (strBinding)
                {
                    case "TCP":
                        strEPAdr = "net.tcp://" + strServer + ":" + nPort.ToString() + "/MathService";
                        ep = new EndpointAddress(strEPAdr);
                        NetTcpBinding tcpb = new NetTcpBinding();
                        channelFactory = new ChannelFactory<IMathService>(tcpb);
                        break;

                    case "HTTP":
                        strEPAdr = "http://" + strServer + ":" + nPort.ToString() + "/MathService";
                        ep = new EndpointAddress(strEPAdr);
                        BasicHttpBinding httpb = new BasicHttpBinding();
                        channelFactory = new ChannelFactory<IMathService>(httpb);
                        break;
                }

                IMathService mathSvcObj = channelFactory.CreateChannel(ep);
                double dblResult = 0;
                switch (strOper)
                {
                    case "ADD": dblResult = mathSvcObj.AddNumber(dblVal1, dblVal2); break;
                    case "SUB": dblResult = mathSvcObj.SubtractNumber(dblVal1, dblVal2); break;
                    case "MUL": dblResult = mathSvcObj.MultiplyNumber(dblVal1, dblVal2); break;
                    case "DIV": dblResult = mathSvcObj.DivideNumber(dblVal1, dblVal2); break;
                }
                
                Console.WriteLine("Operation {0} ", strOper );
                Console.WriteLine("Operand 1 {0} ", dblVal1.ToString ( "F2"));
                Console.WriteLine("Operand 2 {0} ", dblVal2.ToString("F2"));
                Console.WriteLine("Result {0} ", dblResult.ToString("F2"));
                channelFactory.Close();
            }
            catch (Exception eX)
            {
                Console.WriteLine("Error while performing operation [" + eX.Message + "] \n\n Inner Exception [" + eX.InnerException + "]");
            }
        }
    }
}
