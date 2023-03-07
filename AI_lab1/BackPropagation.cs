using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_lab1
{
    internal class BackPropagation
    {
        private static readonly List<double> _synapses = new List<double> { 0.256, 0.420, 0.160, 0.429, 0.117, 0.440, 0.088, 0.414, 0.007, 0.477, 0.195, 0.418, 0.004, 0.505, 0.140 };
        private static readonly double _learningSpeed = 0.1;
        private static List<double> _weights = new List<double>();
        private static int _currentIndex = 0;

        static BackPropagation()
        {
            _randomizeWeights();
        }
        private static void _randomizeWeights()
        {
            Random rnd = new Random();
            for (int i = 0; i < 16; i++)
            {
                _weights.Add(rnd.Next(1, 10) / 10);
            }
        }
        static public void Test(int Index = 0, int epoch = 12)
        {
            Console.WriteLine("\nTest");
            List<double> expected = new List<double>();
            List<double> result= new List<double>();
            _currentIndex= Index;   

            for (int i = 0; i < epoch; i++)
            {
                var results = _backPropagation();
                expected.Add(Math.Round(results["expected"], 4));
                result.Add(Math.Round(results["result"], 4));
                if (_currentIndex >= 11)
                {
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex++;
                }
            }

            Console.WriteLine("Expected: " + String.Join("  ", expected.Select(x => String.Format("{0,-5:0.0000}", x))));
            Console.WriteLine("Result:   " + String.Join("  ", result.Select(x => String.Format("{0,-5:0.0000}", x))));
            Console.WriteLine("Average delta: " + String.Format("{0,-5:0.0000}", _averageDelta(expected, result)));
        }
        static private double _averageDelta(List<double> expected, List<double> result)
        {
            double delta = 0;
            for(int i = 0 ; i < expected.Count; i++)
            {
                delta += Math.Abs(result[i] - expected[i]) / expected.Count;
            }
            return delta ;
        }
        static public void Leanrning()
        {
            _currentIndex = 0;
            Console.WriteLine("\nStart learning...");
            for (int i = 0; i < 10_000_000; i++)
            {
                var results = _backPropagation();
                //Console.WriteLine($"{i} - expected = {results["expected"]} === result = {results["result"]} === delta = {results["delta"]}");
                if (_currentIndex >= 9)
                {
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex++;
                }
            }
            Console.WriteLine("End learning");
        }
        private static Dictionary<string, double> _backPropagation()
        {
            double x1 = _synapses[_currentIndex];
            double x2 = _synapses[_currentIndex + 1];
            double x3 = _synapses[_currentIndex + 2];
            double expected = _synapses[_currentIndex + 3];

            double s4 = x1 * _weights[0] + x2 * _weights[1] + x3 * _weights[2];
            double f4 = _activation(s4);
            double s5 = x1 * _weights[3] + x2 * _weights[4] + x3 * _weights[5];
            double f5 = _activation(s5);
            double s6 = x1 * _weights[6] + x2 * _weights[7] + x3 * _weights[8];
            double f6 = _activation(s6);
            double s7 = x1 * _weights[9] + x2 * _weights[10] + x3 * _weights[11];
            double f7 = _activation(s7);

            double s8 = f4 * _weights[12] + f5 * _weights[13] + f6 * _weights[14] + f7 * _weights[15];
            double f8 = _activation(s8);

            double currentResult = f8;

            double error = expected - currentResult;

            double deltaOutputSum = _derivative_activation(s8) * error;

            double deltaW12 = deltaOutputSum * f4 * _learningSpeed;
            double deltaW13 = deltaOutputSum * f5 * _learningSpeed;
            double deltaW14 = deltaOutputSum * f6 * _learningSpeed;
            double deltaW15 = deltaOutputSum * f7 * _learningSpeed;

            _weights[12] += deltaW12;
            _weights[13] += deltaW13;
            _weights[14] += deltaW14;
            _weights[15] += deltaW15;

            double deltaHiddenSum4 = deltaOutputSum * _weights[12] * _derivative_activation(s4);
            double deltaHiddenSum5 = deltaOutputSum * _weights[13] * _derivative_activation(s5);
            double deltaHiddenSum6 = deltaOutputSum * _weights[14] * _derivative_activation(s6);
            double deltaHiddenSum7 = deltaOutputSum * _weights[15] * _derivative_activation(s7);

            double deltaWeight0 = deltaHiddenSum4 * x1 * _learningSpeed;
            double deltaWeight1 = deltaHiddenSum4 * x2 * _learningSpeed;
            double deltaWeight2 = deltaHiddenSum4 * x3 * _learningSpeed;
            double deltaWeight3 = deltaHiddenSum5 * x1 * _learningSpeed;
            double deltaWeight4 = deltaHiddenSum5 * x2 * _learningSpeed;
            double deltaWeight5 = deltaHiddenSum5 * x3 * _learningSpeed;
            double deltaWeight6 = deltaHiddenSum6 * x1 * _learningSpeed;
            double deltaWeight7 = deltaHiddenSum6 * x2 * _learningSpeed;
            double deltaWeight8 = deltaHiddenSum6 * x3 * _learningSpeed;
            double deltaWeight9 = deltaHiddenSum7 * x1 * _learningSpeed;
            double deltaWeight10 = deltaHiddenSum7 * x2 * _learningSpeed;
            double deltaWeight11 = deltaHiddenSum7 * x3 * _learningSpeed;

            _weights[0] += deltaWeight0;
            _weights[1] += deltaWeight1;
            _weights[2] += deltaWeight2;
            _weights[3] += deltaWeight3;
            _weights[4] += deltaWeight4;
            _weights[5] += deltaWeight5;
            _weights[6] += deltaWeight6;
            _weights[7] += deltaWeight7;
            _weights[8] += deltaWeight8;
            _weights[9] += deltaWeight9;
            _weights[10] += deltaWeight10;
            _weights[11] += deltaWeight11;

            return new Dictionary<string, double>()
            {
                { "expected", expected},
                { "result", currentResult},
                { "delta", expected - currentResult}                    
            };

        }
        private static double _activation(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        private static double _derivative_activation(double x)
        {
            return _activation(x) * (1 - _activation(x));
        }

    }
}
