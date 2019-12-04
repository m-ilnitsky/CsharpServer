using System;

namespace L2_Task4
{
    class Euclid
    {
        public static int GetGreatestCommonDivisor(int number1, int number2)
        {
            if (number1 == 0 && number2 == 0)
            {
                throw new ArgumentException("Оба числа равны нулю! Найти НОД нельзя!");
            }

            if (number2 == 0)
            {
                return number1;
            }

            var remainder = number1 % number2;

            while (remainder != 0)
            {
                number1 = number2;
                number2 = remainder;
                remainder = number1 % number2;
            }

            return number2;
        }
    }
}
