// Возможно, реализации CalculationErrorPower и CheckArgumentForPositiveValue непосредственно
// в интерфейсе IShape не лучшее решение, но нужно было предотвратить ошибку разработчика
// при добавлении новой фигуры и использования непереопределённого метода GetArea базового класса.
// Также в данном случае кажутся предпочтительными именно структуры.

// Не исключено, что лучше было бы завернуть всю реализацию в один статический класс, но сходу
// пришли на ум только enum и params - выглядело бы не очень.

namespace Calculation
{
    public struct Circle : IShape
    {
        public double Radius { get; }

        public Circle(double radius)
        {
            IShape.CheckLength(radius, nameof(radius));
            Radius = radius;
        }

        public double GetArea() => Math.PI * Radius * Radius;
    }

    public struct Triangle : IShape
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public Triangle(double a, double b, double c)
        {
            CheckSideLengths();
            A = a;
            B = b;
            C = c;

            void CheckSideLengths()
            {
                IShape.CheckLength(a, nameof(a));
                IShape.CheckLength(b, nameof(b));
                IShape.CheckLength(c, nameof(c));

                if (a > b + c || b > a + c || c > a + b)
                    throw new ArgumentException("Wrong side lengths: not a triangle shape.");
            }
        }

        public double GetArea()
        {
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public bool IsTriangleRight()
        {
            double[] poweredSides = new double[3] { A * A, B * B, C * C };
            Array.Sort(poweredSides);
            double calculationError = poweredSides[2] * Math.Pow(10.0, -IShape.CalculationErrorPower);
            return Math.Abs(poweredSides[2] - poweredSides[1] - poweredSides[0]) < calculationError;
        }
    }

    public interface IShape
    {
        public double GetArea();

        public static int CalculationErrorPower { get => 9; }

        public static void CheckLength(double argument, string argumentName)
        {
            if (argument <= 0)
                throw new ArgumentException($"{argumentName} is not a positive number.");

            if (double.IsNaN(argument) || double.IsInfinity(argument) || argument is double.MaxValue)
                throw new ArgumentException($"{argumentName} is not a valid number.");
        }
    }
}