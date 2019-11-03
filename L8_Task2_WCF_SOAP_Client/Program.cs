using L8_Task2_WCF_SOAP_Client.ChatService;

namespace L8_Task2_WCF_SOAP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var chatClient = new ChatClient();

            chatClient.Run();
        }
    }
}
