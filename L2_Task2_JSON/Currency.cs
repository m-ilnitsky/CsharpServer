namespace L2_Task2_JSON
{
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            var currency = (Currency)obj;

            if (currency.Code != Code)
            {
                return false;
            }

            if (currency.Name != Name)
            {
                return false;
            }

            return currency.Symbol == Symbol;
        }

        public override int GetHashCode()
        {
            const int prime = 3571;
            var hashCode = 1;

            if (!ReferenceEquals(Code, null))
            {
                hashCode = hashCode * prime + Code.GetHashCode();
            }

            if (!ReferenceEquals(Name, null))
            {
                hashCode = hashCode * prime + Name.GetHashCode();
            }

            if (!ReferenceEquals(Symbol, null))
            {
                hashCode = hashCode * prime + Symbol.GetHashCode();
            }

            return hashCode;
        }
    }
}
