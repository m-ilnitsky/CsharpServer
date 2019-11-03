using System.ServiceModel;

namespace L8_Task2_WCF_SOAP_Service
{
    [ServiceContract]
    public interface IChatService
    {

        [OperationContract]
        bool SendMessage(string name, string secretString, string message);

        [OperationContract]
        ChatMessage[] GetMessages(int startMessageNumber);
    }
}
