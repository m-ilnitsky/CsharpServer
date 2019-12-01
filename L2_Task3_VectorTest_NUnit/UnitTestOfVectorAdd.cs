using NUnit.Framework;

using L2_Task3_Vector;

namespace L2_Task3_VectorTest_NUnit
{
    [TestFixture]
    public class UnitTestOfVectorAdd
    {
        [Test]
        public void TestAddForVectorsWithLength1()
        {
            var vector1 = new Vector(new double[] { 1 });
            var vector2 = new Vector(new double[] { 2 });
            var vectorsSum = new Vector(new double[] { 3 });

            vector1.Add(vector2);

            Assert.IsTrue(vector1.Equals(vectorsSum), "Результирующий вектор " + vector1 + " не равен эталону " + vectorsSum);
        }

        [Test]
        public void TestAddForVectorsWithLength2()
        {
            var vector1 = new Vector(new double[] { 1, 2 });
            var vector2 = new Vector(new double[] { 3, 4 });
            var vectorsSum = new Vector(new double[] { 4, 6 });

            vector1.Add(vector2);

            Assert.IsTrue(vector1.Equals(vectorsSum), "Результирующий вектор " + vector1 + " не равен эталону " + vectorsSum);
        }

        [Test]
        public void TestAddForVectorsWithLength3()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3 });
            var vector2 = new Vector(new double[] { 4, 5, 6 });
            var vectorsSum = new Vector(new double[] { 5, 7, 9 });

            vector1.Add(vector2);

            Assert.IsTrue(vector1.Equals(vectorsSum), "Результирующий вектор " + vector1 + " не равен эталону " + vectorsSum);
        }

        [Test]
        public void TestAddWhereSecondVectorIsLonger()
        {
            var vector1 = new Vector(new double[] { 1, 2 });
            var vector2 = new Vector(new double[] { 3, 4, 5, 6 });
            var vectorsSum = new Vector(new double[] { 4, 6, 5, 6 });

            vector1.Add(vector2);

            Assert.IsTrue(vector1.Equals(vectorsSum), "Результирующий вектор " + vector1 + " не равен эталону " + vectorsSum);
        }

        [Test]
        public void TestAddWhereFirstVectorIsLonger()
        {
            var vector1 = new Vector(new double[] { 3, 4, 5, 6 });
            var vector2 = new Vector(new double[] { 1, 2 });
            var vectorsSum = new Vector(new double[] { 4, 6, 5, 6 });

            vector1.Add(vector2);

            Assert.IsTrue(vector1.Equals(vectorsSum), "Результирующий вектор " + vector1 + " не равен эталону " + vectorsSum);
        }
    }
}
