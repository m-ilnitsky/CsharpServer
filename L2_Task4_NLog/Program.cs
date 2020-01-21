using System;
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
                        try
                        {
                            try
                            {
                                var greatestCommonDivisor = Euclid.GetGreatestCommonDivisor(i, j);

                                if (greatestCommonDivisor != 1)
                                {
                                    Logger.Info($"НОД({i}, {j}) = {greatestCommonDivisor}");
                                }
                            }
                            catch (Exception e)
                            {
                                throw new Exception("2-е исключение-обёртка!", e);
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("3-е исключение-обёртка!", e);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e, $"Для чисел {i} и {j} возникло исключение в функции Euclid.GetGreatestCommonDivisor: {e.Message}");
                    }
                }
            }

            Logger.Info("Работа выполнена");

            Console.ReadKey();
        }
    }
}
