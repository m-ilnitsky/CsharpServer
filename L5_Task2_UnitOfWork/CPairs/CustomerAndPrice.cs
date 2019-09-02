using System.Text;
using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Pairs
{
    public class CustomerAndPrice
    {
        public Customer Customer { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var date = (Customer.Birthday != null) ? Customer.Birthday.Value.Date.ToString("d") : "";

            stringBuilder.AppendFormat("Id:{0,4} Name: {1,-20}   Birthday: {2,10}   Price: {3}", Customer.Id, Customer.Surname + " " + Customer.Name, date, Price);

            return stringBuilder.ToString();
        }
    }
}
