using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
 
namespace ConWCFMathClient
{
    [ServiceContract]
    public interface IMathService
    {
        [OperationContract]
        double AddNumber(double dblX, double dblY);
        [OperationContract]
        double SubtractNumber(double dblX, double dblY);
        [OperationContract]
        double MultiplyNumber(double dblX, double dblY);
        [OperationContract]
        double DivideNumber(double dblX, double dblY);
    }
}
