using System;
using NUnit.Framework;

using L2_Task3_Vector;

namespace L2_Task3_VectorTest_NUnit
{
    [TestFixture]
    public class UnitTestOfVectorGetScalarProduct
    {
        private static bool Equals(double value1, double value2)
        {
            return Math.Abs(value1 - value2) <= 1e-10;
        }

        [Test]
        public void TestGetScalarProductForVectorsWithLength1()
        {
            var vector1 = new Vector(new double[] { 1 });
            var vector2 = new Vector(new double[] { 3 });

            var trueResult = 3d;
            var currentResult = Vector.GetScalarProduct(vector1, vector2);

            Assert.IsTrue(Equals(trueResult, currentResult), "Результат скалярного произведения (" + currentResult + ") не равен эталону (" + trueResult + ")");
        }

        [Test]
        public void TestGetScalarProductForVectorsWithLength2()
        {
            var vector1 = new Vector(new double[] { 1, 3 });
            var vector2 = new Vector(new double[] { 2, 4 });

            var trueResult = 14d;
            var currentResult = Vector.GetScalarProduct(vector1, vector2);

            Assert.IsTrue(Equals(trueResult, currentResult), "Результат скалярного произведения (" + currentResult + ") не равен эталону (" + trueResult + ")");
        }

        [Test]
        public void TestGetScalarProductForVectorsWithLength3()
        {
            var vector1 = new Vector(new double[] { 1, 3, 5 });
            var vector2 = new Vector(new double[] { 2, 4, 6 });

            var trueResult = 44d;
            var currentResult = Vector.GetScalarProduct(vector1, vector2);

            Assert.IsTrue(Equals(trueResult, currentResult), "Результат скалярного произведения (" + currentResult + ") не равен эталону (" + trueResult + ")");
        }

        [Test]
        public void TestGetScalarProductWhereFirstVectorIsLonger()
        {
            var vector1 = new Vector(new double[] { 1, 3, 5 });
            var vector2 = new Vector(new double[] { 2, 4 });

            var trueResult = 14d;
            var currentResult = Vector.GetScalarProduct(vector1, vector2);

            Assert.IsTrue(Equals(trueResult, currentResult), "Результат скалярного произведения (" + currentResult + ") не равен эталону (" + trueResult + ")");
        }

        [Test]
        public void TestGetScalarProductWhereSecondVectorIsLonger()
        {
            var vector1 = new Vector(new double[] { 1, 3 });
            var vector2 = new Vector(new double[] { 2, 4, 6 });

            var trueResult = 14d;
            var currentResult = Vector.GetScalarProduct(vector1, vector2);

            Assert.IsTrue(Equals(trueResult, currentResult), "Результат скалярного произведения (" + currentResult + ") не равен эталону (" + trueResult + ")");
        }
    }
}
