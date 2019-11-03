using System;
using System.Runtime.Serialization;

namespace L8_Task2_WCF_SOAP_Service
{
    [DataContract]
    public class ChatMessage
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Number { get; set; }
    }
}