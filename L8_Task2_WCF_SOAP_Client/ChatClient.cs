using L8_Task2_WCF_SOAP_Client.ChatService;
using System;
using System.Collections.Generic;

namespace L8_Task2_WCF_SOAP_Client
{
    public class ChatClient
    {
        private List<ChatMessage> chatMessages = new List<ChatMessage>(256);
        private string userName;
        private string userSecretString;

        private T Call<T>(Func<ChatServiceClient, T> f)
        {
            var chatClient = new ChatServiceClient();

            try
            {
                var result = f(chatClient);
                chatClient.Close();

                return result;
            }
            catch (Exception e)
            {
                chatClient.Abort();
                throw e;
            }
        }

        public bool SendMessage(string name, string secretString, string message)
        {
            return Call(c => c.SendMessage(name, secretString, message));
        }

        public bool SendMessage(string message)
        {
            return SendMessage(userName, userSecretString, message);
        }

        public void LoadMessages()
        {
            var lastMessages = Call(c => c.GetMessages(chatMessages.Count));
            chatMessages.AddRange(lastMessages);
        }

        public void PrintMessages()
        {
            Console.Clear();

            foreach (var message in chatMessages)
            {
                if (message.Name == "System")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (userName != null && message.Name == userName)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.WriteLine("[{0:T}] {1}: {2}", message.Date, message.Name, message.Message);
            }
        }

        public void SetSystemColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public string ReadMessage()
        {
            SetSystemColor();
            Console.WriteLine("Введите сообщение или команду exit для выхода:");
            return Console.ReadLine().Trim();
        }

        public bool LoginToChat()
        {
            Console.Write("Введите ваше имя: ");
            var name = Console.ReadLine();

            Console.Write("Введите секретную строку для вашей идентификации (придумайте её): ");
            var secretString = Console.ReadLine();

            var isLogin = SendMessage(name, secretString, "Привет!");

            if (isLogin)
            {
                userName = name;
                userSecretString = secretString;
            }

            return isLogin;
        }

        public void Run()
        {
            var isRunning = true;

            while (isRunning)
            {
                LoadMessages();
                PrintMessages();
                SetSystemColor();

                if (userName == null)
                {
                    Console.WriteLine("Введите команду exit для выхода или input для входа");
                    var command = Console.ReadLine().Trim();

                    if (command == "input")
                    {
                        LoginToChat();
                    }
                    else if (command == "exit")
                    {
                        isRunning = false;
                    }
                }
                else
                {
                    var message = ReadMessage();

                    if (message == "exit")
                    {
                        isRunning = false;
                    }
                    else if (message != "")
                    {
                        SendMessage(message);
                    }
                }
            }

            Console.WriteLine("Всего доброго!");
            Console.ReadKey();
        }
    }
}
