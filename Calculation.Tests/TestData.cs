namespace Calculation.Tests
{
    public static class TestData
    {
        public static Circle[] Circles { get; } =
        {
            new Circle(1, 3.141592653589793),
            new Circle(0.01, 0.0003141592653589793),
            new Circle(0.000001, 3.141592653589793e-12),
            new Circle(113, 40114.99659368807),
            new Circle(1000001, 3141598936778.2417),
        };

        public static double[] NotValidCircleRadiuses { get; } =
        {
            0,
            -0.01,
            double.MaxValue,
            double.NaN,
        };

        public static Triangle[] NotRightTriangles { get; } =
        {
            new Triangle(1, 1, 1, 0.4330127018922193, false),
            new Triangle(0.1, 0.35, 0.3, 0.013905372163304374, false),
            new Triangle(0.0000001, 0.00000035, 0.0000003, 1.3905372163304378e-14, false),
            new Triangle(113, 202, 98, 3070.343455950816, false),
            new Triangle(1000001, 500001, 500002, 500001000, false),
        };

        public static double[][] NotValidTriangleSides { get; } =
        {
            new double[] { 1, 1, 0 },
            new double[] { 0.1, -0.35, 0.3 },
            new double[] { 0.0000001, double.MaxValue, 0.0000003 },
            new double[] { 0.0000001, 0.00000035, double.NaN },
            new double[] { double.PositiveInfinity, 0.00000035, 0.0000003 },
            new double[] { 113, 1130, 98 },
        };

        public static Triangle[] RightTriangles { get; } =
        {
            new Triangle(4, 5, 3, 6, true),
            new Triangle(964.22509537, 0.03249845, 964.22509592, 15.667910525281998, true),
        };

        public struct Circle
        {
            public double Radius { get; }
            public double Area { get; }

            public Circle(double radius, double area)
            {
                Radius = radius;
                Area = area;
            }
        }

        public struct Triangle
        {
            public double A { get; }
            public double B { get; }
            public double C { get; }
            public double Area { get; }
            public bool IsRightTriangle { get; }

            public Triangle(double a, double b, double c, double area, bool isRightTriangle)
            {
                A = a;
                B = b;
                C = c;
                Area = area;
                IsRightTriangle = isRightTriangle;
            }
        }
    }
}
