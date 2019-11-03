using System;
using System.Collections.Generic;
using System.Linq;

namespace L8_Task2_WCF_SOAP_Service
{
    public class ChatService : IChatService
    {
        private static Dictionary<string, UserData> users = new Dictionary<string, UserData>();
        private static List<ChatMessage> messages = new List<ChatMessage>();

        public ChatService() : base()
        {
            if (messages.Count == 0)
            {
                AddSystemMessage("Всем добрый день!");
            }
        }

        private void AddMessage(string name, string message)
        {
            messages.Add(new ChatMessage
            {
                Name = name,
                Message = message,
                Date = DateTime.Now,
                Number = messages.Count
            });
        }

        private void AddSystemMessage(string message)
        {
            AddMessage("System", message);
        }

        public bool SendMessage(string name, string secretString, string message)
        {
            if (name.Trim() != name)
            {
                AddSystemMessage("Попытка войти с именем '" + name + "' содержащим пробелы");
                return false;
            }
            if (name.ToLower() == "system")
            {
                AddSystemMessage("Попытка войти с именем '" + name + "' маскирующимся под систему");
                return false;
            }
            if (users.ContainsKey(name) && users[name].SecretString != secretString)
            {
                AddSystemMessage("Попытка войти под чужим именем '" + name + "'");
                return false;
            }

            if (!users.ContainsKey(name))
            {
                AddSystemMessage("У нас новый пользователь! Добро пожаловать '" + name + "'!");
                users.Add(name, new UserData(name, secretString));
            }

            AddMessage(name, message);

            return true;
        }

        public ChatMessage[] GetMessages(int startMessageNumber)
        {
            return messages.Where(m => m.Number >= startMessageNumber).ToArray();
        }
    }
}
