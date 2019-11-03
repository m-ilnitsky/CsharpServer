namespace L8_Task2_WCF_SOAP_Service
{
    public class UserData
    {
        public UserData(string name, string secretString)
        {
            Name = name;
            SecretString = secretString;
        }

        public string Name { get; }

        public string SecretString { get; }
    }
}