using System;

namespace _Scripts.Helpers
{
    public static class GaussianRandomNumberGenerator
    {
        private static readonly Random random = new();

        public static double GenerateRandomNumber(double mean, double standardDeviation)
        {
            var u1 = 1.0 - random.NextDouble();
            var u2 = 1.0 - random.NextDouble();

            var z0 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
            
            return mean + standardDeviation * z0;
        }
    }
}