using System;
using System.Collections.Generic;
using NLog;

namespace L2_Task4
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var minNumber = 0;
            var maxNumber = 50;

            Logger.Info($"Вычисление НОД для пар чисел от {minNumber} до {maxNumber}:");

            for (var i = minNumber; i <= maxNumber; i++)
            {
                for (var j = minNumber; j <= i; j++)
                {
                    try
                    {
                        var greatestCommonDivisor = Euclid.GetGreatestCommonDivisor(i, j);

                        if (greatestCommonDivisor != 1)
                        {
                            Logger.Info($"НОД({i}, {j}) = {greatestCommonDivisor}");
                        }
                    }
                    catch (Exception exc)
                    {
                        var exceptions = new LinkedList<Exception>();
                        Exception innerException = exc;

                        while (innerException != null)
                        {
                            exceptions.AddFirst(innerException);
                            innerException = innerException.InnerException;
                        }

                        foreach (var e in exceptions)
                        {
                            Logger.Error(e, $"Для чисел {i} и {j} возникло исключение в функции Euclid.GetGreatestCommonDivisor: {e.Message}\nStackTrace:\n{e.StackTrace}");
                        }
                    }
                }
            }

            Logger.Info("Работа выполнена");

            Console.ReadKey();
        }
    }
}
