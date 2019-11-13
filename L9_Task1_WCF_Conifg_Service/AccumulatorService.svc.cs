namespace L9_Task1_WCF_Conifg_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AccumulatorService : IAccumulatorService
    {
        private static int sum = 0;

        public void Add(int value)
        {
            sum += value;
        }

        public int GetSum()
        {
            return sum;
        }
    }
}
