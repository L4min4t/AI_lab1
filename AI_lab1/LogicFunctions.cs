namespace AI_lab1
{
    public class LogicFunctions
    {
        public static int AND(int a, int b)
        {
            return _weightedAmount(a, 1, b, 1) >= 1.5 ? 1 : 0;
        }

        public static int OR(int a, int b)
        {
            return _weightedAmount(a, 1, b, 1) >= 0.5 ? 1 : 0;
        }

        public static int NOT(int a)
        {
            return _weightedAmount(a, -1.5) >= -1 ? 1 : 0;
        }

        public static int XOR(int a, int b)
        {
            int y1 = _weightedAmount(a, 1, b, -1) >= 0.5 ? 1 : 0;
            int y2 = _weightedAmount(a, -1, b, 1) >= 0.5 ? 1 : 0;
            return _weightedAmount(y1, 1, y2, 1) >= 0.5 ? 1 : 0;
        }

        private static double _weightedAmount(int a, double weightA, int b = 0, double weightB=0)
        {
            return a * weightA + b * weightB;
        }
    }
}
