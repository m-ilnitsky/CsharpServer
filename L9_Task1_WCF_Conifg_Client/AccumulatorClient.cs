using System;

using L9_Task1_WCF_Conifg_Client.AccumulatorService;

namespace L9_Task1_WCF_Conifg_Client
{
    class AccumulatorClient
    {
        private static int GetSum(string endpointName)
        {
            var chatClient = new AccumulatorServiceClient(endpointName);

            try
            {
                var result = chatClient.GetSum();
                chatClient.Close();

                return result;
            }
            catch (Exception e)
            {
                chatClient.Abort();
                throw e;
            }
        }

        private static void Add(string endpointName, int value)
        {
            var chatClient = new AccumulatorServiceClient(endpointName);

            try
            {
                chatClient.Add(value);
                chatClient.Close();
            }
            catch (Exception e)
            {
                chatClient.Abort();
                throw e;
            }
        }

        public static void Run()
        {
            var endpointNames = new string[]
            {
                "HttpBinding_IAccumulatorService",
                "ShttpBinding_IAccumulatorService",
                "NetTcpBinding_IAccumulatorService",
                "NetNamedPipeBinding_IAccumulatorService"
            };

            Console.WriteLine("Start!");
            Console.WriteLine();
            Console.WriteLine();

            int count = 1;

            foreach (var endpointName in endpointNames)
            {
                Console.WriteLine(endpointName + ":");

                var accumulator = GetSum(endpointName);
                Console.WriteLine("Accumulator = {0}", accumulator);

                Add(endpointName, count);
                Console.WriteLine("Add({0})", count);

                accumulator = GetSum(endpointName);
                Console.WriteLine("Accumulator = {0}", accumulator);

                Console.WriteLine();
                Console.WriteLine();

                count++;
            }

            Console.WriteLine("The End!");
            Console.ReadKey();
        }
    }
}
