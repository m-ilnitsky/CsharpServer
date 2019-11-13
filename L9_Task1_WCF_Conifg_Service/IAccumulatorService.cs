using System.ServiceModel;

namespace L9_Task1_WCF_Conifg_Service
{
    [ServiceContract]
    public interface IAccumulatorService
    {
        [OperationContract]
        void Add(int value);

        [OperationContract]
        int GetSum();
    }
}
