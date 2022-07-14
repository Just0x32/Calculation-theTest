using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Calculation.Tests
{
    public class ShapesTests
    {
        private static int calculationErrorPower = 12;

        private static IEnumerable<TestCaseData> ValidShapesSource()
        {
            CheckArray(TestData.Circles, nameof(TestData.Circles));
            CheckArray(TestData.NotRightTriangles, nameof(TestData.NotRightTriangles));
            CheckArray(TestData.RightTriangles, nameof(TestData.RightTriangles));

            foreach (var shape in TestData.Circles)
                yield return new TestCaseData(new Circle(shape.Radius), shape.Area);

            foreach (var shape in TestData.NotRightTriangles)
                yield return new TestCaseData(new Triangle(shape.A, shape.B, shape.C), shape.Area);

            foreach (var shape in TestData.RightTriangles)
                yield return new TestCaseData(new Triangle(shape.A, shape.B, shape.C), shape.Area);
        }

        [Test, TestCaseSource(nameof(ValidShapesSource))]
        public void GetArea_ValidShapes_True(IShape shape, double validArea)
        {
            double result = shape.GetArea();
            Assert.True(IsResultValid(result, validArea), ResultIsNotValidValueErrorMessage(result, validArea));
        }

        private static IEnumerable<TestCaseData> NotValidCircleRadiusesSource()
        {
            CheckArray(TestData.NotValidCircleRadiuses, nameof(TestData.NotValidCircleRadiuses));

            foreach (var radius in TestData.NotValidCircleRadiuses)
                yield return new TestCaseData(radius);
        }

        [Test, TestCaseSource(nameof(NotValidCircleRadiusesSource))]
        public void CircleInitialize_NotValidCircleRadiuses_ArgumentException(double radius)
        {
            Assert.Throws<ArgumentException>(() => new Circle(radius));
        }

        private static IEnumerable<TestCaseData> NotValidTriangleSidesSource()
        {
            CheckArray(TestData.NotValidTriangleSides, nameof(TestData.NotValidTriangleSides));

            for (int i = 0; i < TestData.NotValidTriangleSides.Length; i++)
            {
                CheckArray(TestData.NotValidTriangleSides[i], nameof(TestData.NotValidTriangleSides) + $"[{i}]", 3);
                yield return new TestCaseData(TestData.NotValidTriangleSides[i][0],
                    TestData.NotValidTriangleSides[i][1], TestData.NotValidTriangleSides[i][2]);
            }
        }

        [Test, TestCaseSource(nameof(NotValidTriangleSidesSource))]
        public void TriangleInitialize_NotValidTriangleSides_ArgumentException(double a, double b, double c)
        {
            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
        }

        private static IEnumerable<TestCaseData> NotRightTrianglesSource()
        {
            CheckArray(TestData.NotRightTriangles, nameof(TestData.NotRightTriangles));

            foreach (var shape in TestData.NotRightTriangles)
                yield return new TestCaseData(new Triangle(shape.A, shape.B, shape.C));
        }

        [Test, TestCaseSource(nameof(NotRightTrianglesSource))]
        public void IsTriangleRight_NotRightTriangles_False(Triangle triangle)
        {
            bool result = triangle.IsTriangleRight();
            Assert.False(result);
        }

        private static IEnumerable<TestCaseData> RightTrianglesSource()
        {
            CheckArray(TestData.RightTriangles, nameof(TestData.RightTriangles));

            foreach (var shape in TestData.RightTriangles)
                yield return new TestCaseData(new Triangle(shape.A, shape.B, shape.C));
        }

        [Test, TestCaseSource(nameof(RightTrianglesSource))]
        public void IsTriangleRight_RightTriangles_True(Triangle triangle)
        {
            bool result = triangle.IsTriangleRight();
            Assert.True(result);
        }

        private static void CheckArray(dynamic array, string arrayName, int? arrayLength = null)
        {
            if (array is null)
                throw new ArgumentNullException($"{arrayName} is null.");

            if (!(array is Array))
                throw new ArgumentException($"{arrayName} is not an array.");

            if (array.Length == 0)
                throw new ArgumentException($"{arrayName} has no items.");

            if (arrayLength != null && array.Length != arrayLength)
                throw new ArgumentException($"{arrayName} length is not {arrayLength}: {array.Length}");
        }

        private static bool IsResultValid(double result, double validValue)
        {
            double calculationError = Math.Max(result, validValue) * Math.Pow(10, -calculationErrorPower);
            return Math.Abs(result - validValue) < calculationError;
        }

        private static string ResultIsNotValidValueErrorMessage(double result, double validValue)
            => $"{result} is not {validValue}.";
    }
}