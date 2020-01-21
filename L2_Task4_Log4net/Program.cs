using System;
using System.Collections.Generic;
using log4net;
using log4net.Config;

namespace L2_Task4
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

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
                        Logger.Error($"Для чисел {i} и {j} возникло исключение в функции Euclid.GetGreatestCommonDivisor: {e.Message}", e);
                    }
                }
            }

            Logger.Info("Работа выполнена");

            Console.ReadKey();
        }
    }
}
