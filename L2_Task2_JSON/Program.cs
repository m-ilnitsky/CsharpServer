﻿using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace L2_Task2_JSON
{
    internal class Program
    {
        private static void Main()
        {
            const string filePath = @"..\..\americas.json";

            using (var reader = new StreamReader(filePath))
            {
                var jsonString = reader.ReadToEnd();
                var countries = JsonConvert.DeserializeObject<List<Country>>(jsonString);

                var currenciesSet = new HashSet<Currency>();

                Console.WriteLine("Список стран:");

                var count = 0;
                var totalPopulation = 0;

                foreach (var country in countries)
                {
                    Console.Write("{0,2}. Страна: {1},  Население(чел.): {2},  Валюты: [", ++count, country.Name, country.Population);

                    for (var i = 0; i < country.Currencies.Count; ++i)
                    {
                        Console.Write(country.Currencies[i].Name);

                        if (i + 1 < country.Currencies.Count)
                        {
                            Console.Write(",  ");
                        }

                        currenciesSet.Add(country.Currencies[i]);
                    }

                    Console.WriteLine("]");

                    totalPopulation += country.Population;
                }

                Console.WriteLine();
                Console.WriteLine("Суммарная численность населения (мил.чел.): {0}", totalPopulation / 1000000);

                var currenciesList = new List<Currency>(currenciesSet);
                currenciesList.Sort((currency1, currency2) =>
                {
                    if (currency1.Name == null && currency2.Name == null) return 0;
                    if (currency1.Name == null) return -1;
                    if (currency2.Name == null) return 1;

                    return currency1.Name.CompareTo(currency2.Name);
                });

                Console.WriteLine();
                Console.WriteLine("Набор валют:");

                count = 0;
                foreach (var currency in currenciesList)
                {
                    Console.WriteLine("{0,2}. Название: {1,-30}    Код:{2,6}    Символ: {3}", ++count, currency.Name, currency.Code, currency.Symbol);
                }

                Console.ReadKey();
            }
        }
    }
}
