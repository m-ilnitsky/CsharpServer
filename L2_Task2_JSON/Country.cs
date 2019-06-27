using System.Collections.Generic;

namespace L2_Task2_JSON
{
    public class Country
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public List<Currency> Currencies { get; set; }
    }
}
