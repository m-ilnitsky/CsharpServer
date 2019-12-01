using System;
using System.Text;

namespace L2_Task3_Vector
{
    public class Vector
    {
        private double[] _elements;

        public Vector(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Size must be > 0 (Size=" + length + ").", nameof(length));
            }

            _elements = new double[length];
        }

        public Vector(Vector vector)
        {
            _elements = new double[vector._elements.Length];

            Array.Copy(vector._elements, _elements, vector._elements.Length);
        }

        public Vector(double[] values)
        {
            if (values.Length <= 0)
            {
                throw new ArgumentException("Size must be > 0 (Size=" + values.Length + ").", nameof(values.Length));
            }

            _elements = new double[values.Length];

            Array.Copy(values, _elements, values.Length);
        }

        public Vector(int length, double[] values)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Size must be > 0 (Size=" + length + ").", nameof(length));
            }

            _elements = new double[length];

            var copyLength = Math.Min(length, values.Length);

            Array.Copy(values, _elements, copyLength);
        }

        public int GetSize()
        {
            return _elements.Length;
        }

        public double GetLength()
        {
            var result = 0d;

            foreach (var element in _elements)
            {
                result += element * element;
            }

            return Math.Sqrt(result);
        }

        private void TestIndex(int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index < 0  (Index = " + index + ").");
            }
            if (index >= _elements.Length)
            {
                throw new IndexOutOfRangeException("Index >= Length  (Index = " + index + ", Length = " + _elements.Length + ").");
            }
        }

        public double GetElement(int index)
        {
            TestIndex(index);

            return _elements[index];
        }

        public void SetElement(int index, double value)
        {
            TestIndex(index);

            _elements[index] = value;
        }

        private void ResizeToVector(Vector vector)
        {
            if (vector._elements.Length > _elements.Length)
            {
                Array.Resize(ref _elements, vector._elements.Length);
            }
        }

        public void Add(Vector vector)
        {
            ResizeToVector(vector);

            for (var i = 0; i < vector._elements.Length; ++i)
            {
                _elements[i] += vector._elements[i];
            }
        }

        public void Subtract(Vector vector)
        {
            ResizeToVector(vector);

            for (var i = 0; i < vector._elements.Length; ++i)
            {
                _elements[i] -= vector._elements[i];
            }
        }

        public void Multiply(double scalar)
        {
            for (var i = 0; i < _elements.Length; ++i)
            {
                _elements[i] *= scalar;
            }
        }

        public void Turn()
        {
            Multiply(-1);
        }

        public static Vector GetSum(Vector vector1, Vector vector2)
        {
            var result = new Vector(Math.Max(vector1._elements.Length, vector2._elements.Length), vector1._elements);

            result.Add(vector2);

            return result;
        }

        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            var result = new Vector(Math.Max(vector1._elements.Length, vector2._elements.Length), vector1._elements);

            result.Subtract(vector2);

            return result;
        }

        public static double GetScalarProduct(Vector vector1, Vector vector2)
        {
            var result = 0d;

            var minLength = Math.Min(vector1._elements.Length, vector2._elements.Length);

            for (var i = 0; i < minLength; ++i)
            {
                result += vector1._elements[i] * vector2._elements[i];
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }
            if (ReferenceEquals(obj, null) || obj.GetType() != this.GetType())
            {
                return false;
            }

            var vector = (Vector)obj;

            if (vector._elements.Length != this._elements.Length)
            {
                return false;
            }

            for (var i = 0; i < _elements.Length; ++i)
            {
                if (_elements[i] != vector._elements[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var prime = 3571;
            var hashCode = 1;

            hashCode = hashCode * prime + _elements.Length;

            foreach (var element in _elements)
            {
                hashCode = hashCode * prime + element.GetHashCode();
            }

            return hashCode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("[");
            foreach (var element in _elements)
            {
                sb.Append(element);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            return sb.ToString();
        }
    }
}
