using System;

namespace L2_Task3_Vector
{
    class VectorTest
    {
        static void Main(string[] args)
        {
            double[] values = { 1, 2, 3, 4, 5, 6, 7 };

            var vector1 = new Vector(5);
            var vector2 = new Vector(values);
            var vector3 = new Vector(6, values);
            var vector4 = new Vector(vector2);

            vector3.SetElement(3, 7);

            Console.WriteLine("vector1 = {0}", vector1);
            Console.WriteLine("vector2 = {0}", vector2);
            Console.WriteLine("vector3 = {0}", vector3);
            Console.WriteLine("vector4 = {0}", vector4);

            vector3.Multiply(0.5);

            Console.WriteLine();
            Console.WriteLine("vector3 = vector3 * 0.5 = {0}", vector3);

            var vector5 = Vector.GetSum(vector3, vector2);
            var vector6 = Vector.GetDifference(vector3, vector2);
            var vector7 = Vector.GetSum(vector2, vector1);
            var vector8 = Vector.GetDifference(vector2, vector1);

            Console.WriteLine();
            Console.WriteLine("Static Methods:");
            Console.WriteLine("vector5 = vector3 + vector2 = {0}", vector5);
            Console.WriteLine("vector6 = vector3 - vector2 = {0}", vector6);
            Console.WriteLine("vector7 = vector2 + vector1 = {0}", vector7);
            Console.WriteLine("vector8 = vector2 - vector1 = {0}", vector8);

            var vector9 = new Vector(vector3);
            var vector10 = new Vector(vector3);
            var vector11 = new Vector(vector2);
            var vector12 = new Vector(vector2);

            vector9.Add(vector2);
            vector10.Subtract(vector2);
            vector11.Add(vector1);
            vector12.Subtract(vector1);

            Console.WriteLine();
            Console.WriteLine("Nonstatic Methods:");
            Console.WriteLine("vector9  = vector3 + vector2 = {0}", vector9);
            Console.WriteLine("vector10 = vector3 - vector2 = {0}", vector10);
            Console.WriteLine("vector11 = vector2 + vector1 = {0}", vector11);
            Console.WriteLine("vector12 = vector2 - vector1 = {0}", vector12);

            var scalarProduct1 = Vector.GetScalarProduct(vector3, vector2);
            var scalarProduct2 = Vector.GetScalarProduct(vector5, vector6);
            var scalarProduct3 = Vector.GetScalarProduct(vector2, vector1);

            Console.WriteLine();
            Console.WriteLine("scalarProduct1 = vector3 * vector2 = {0}", scalarProduct1);
            Console.WriteLine("scalarProduct2 = vector5 * vector6 = {0}", scalarProduct2);
            Console.WriteLine("scalarProduct3 = vector2 * vector1 = {0}", scalarProduct3);

            Console.WriteLine();
            Console.WriteLine("Exit?");
            Console.ReadLine();
        }
    }
}
