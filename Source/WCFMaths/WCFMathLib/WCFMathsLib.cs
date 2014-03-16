using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFMathLib
{
    // IMathInterface.cs 
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

    // MathInterface.cs 
    public class MathService : IMathService
    {
        public double AddNumber(double dblX, double dblY)
        {
            return (dblX + dblY);
        }

        public double SubtractNumber(double dblX, double dblY)
        {
            return (dblX - dblY);
        }

        public double MultiplyNumber(double dblX, double dblY)
        {
            return (dblX * dblY);
        }

        public double DivideNumber(double dblX, double dblY)
        {
            return ((dblY == 0) ? 0 : dblX / dblY);
        }
    }
}
