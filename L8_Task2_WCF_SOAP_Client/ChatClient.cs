using L8_Task2_WCF_SOAP_Client.ChatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L8_Task2_WCF_SOAP_Client
{
    public class ChatClient
    {
        private List<ChatMessage> chatMessages = new List<ChatMessage>(256);
        private string userName;
        private string userSecretString;

        public bool SendMessage(string name, string secretString, string message)
        {
            var chatClient = new ChatServiceClient();

            try
            {
                var wasSended = chatClient.SendMessage(name, secretString, message);
                chatClient.Close();

                return wasSended;
            }
            catch (Exception)
            {
                chatClient.Abort();
            }

            return false;
        }

        public bool SendMessage(string message)
        {
            return SendMessage(userName, userSecretString, message);
        }

        public void LoadMessages()
        {
            var chatClient = new ChatServiceClient();

            try
            {
                var lastMessages = chatClient.GetMessages(chatMessages.Count);
                chatMessages.AddRange(lastMessages);
                chatClient.Close();
            }
            catch (Exception)
            {
                chatClient.Abort();
            }
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
            string name = Console.ReadLine();

            Console.Write("Введите секретную строку для вашей идентификации (придумайте её): ");
            string secretString = Console.ReadLine();

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
            bool isRunning = true;

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
                        Console.WriteLine("Всего доброго!");
                    }
                }
                else
                {
                    var message = ReadMessage();

                    if (message == "exit")
                    {
                        isRunning = false;
                        Console.WriteLine("Всего доброго!");
                    }
                    else if (message != "")
                    {
                        SendMessage(message);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
