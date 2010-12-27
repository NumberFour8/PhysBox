using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysLib
{
    public class Integrator
    {
        public delegate Vector Integrand(double X,Vector Y);
        
        public IntegrationMethod Method;
        public double Step;
         
        public Integrator(double StepSize,IntegrationMethod IntType = IntegrationMethod.RungeKutta4)
        {
            Method = IntType;
            Step = StepSize;
        }

        public Vector Integrate(Integrand Function,double X0 = 0,Vector Y0 = null)
        {
            if (Y0 == null) Y0 = Vector.Zero;

            if (Method == IntegrationMethod.RungeKutta4)
            {
                Vector k1 = Function(X0, Y0);
                Vector k2 = 2 * Function(X0 + Step / 2, Y0 + k1 / 2);
                Vector k3 = 2 * Function(X0 + Step / 2, Y0 + k2 / 2);
                Vector k4 = Function(X0 + Step, Y0 + k3);

                return Y0 + Step * (k1 + k2 + k3 + k4) / 6;
            }
            else if (Method == IntegrationMethod.Euler)
            {
                return Y0 + Function(X0 + Step, Y0) * Step;
            }
            else throw new NotImplementedException();
        }

    }

    public enum IntegrationMethod
    {
        RungeKutta4 = 1,Euler = 2,Verlet = 3
    }
}
