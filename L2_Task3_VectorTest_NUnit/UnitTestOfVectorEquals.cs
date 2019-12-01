using NUnit.Framework;

using L2_Task3_Vector;

namespace L2_Task3_VectorTest_NUnit
{
    [TestFixture]
    public class UnitTestOfVectorEquals
    {
        [Test]
        public void TestEqualsOfVectors()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3, 4 });
            var vector2 = new Vector(vector1);

            Assert.IsTrue(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }

        [Test]
        public void TestEqualsOfVectorsWithLength1()
        {
            var vector1 = new Vector(new double[] { 1 });
            var vector2 = new Vector(new double[] { 1 });

            Assert.IsTrue(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }

        [Test]
        public void TestEqualsOfVectorsWithLength2()
        {
            var vector1 = new Vector(new double[] { 1, 2 });
            var vector2 = new Vector(new double[] { 1, 2 });

            Assert.IsTrue(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }

        [Test]
        public void TestEqualsOfVectorsWithLength3()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3 });
            var vector2 = new Vector(new double[] { 1, 2, 3 });

            Assert.IsTrue(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }

        [Test]
        public void TestNotEqualsOfVectorsWithDiffElements()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3 });
            var vector2 = new Vector(new double[] { 3, 4, 5 });

            Assert.IsFalse(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }

        [Test]
        public void TestNotEqualsOfVectorsWhereSecondVectorIsLonger()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3 });
            var vector2 = new Vector(new double[] { 1, 2, 3, 4 });

            Assert.IsFalse(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }

        [Test]
        public void TestNotEqualsOfVectorsWhereFirstVectorIsLonger()
        {
            var vector1 = new Vector(new double[] { 1, 2, 3, 4 });
            var vector2 = new Vector(new double[] { 1, 2, 3 });

            Assert.IsFalse(vector1.Equals(vector2), "Вектор1 " + vector1 + " не равен вектору2 " + vector2);
        }
    }
}
