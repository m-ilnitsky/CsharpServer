using System.ComponentModel.DataAnnotations;

namespace Test_HandlingConcurrencyConflicts
{
    public class Person
    {
        public int PersonId { get; set; }

        [ConcurrencyCheck]
        public string FirstName { get; set; }

        [ConcurrencyCheck]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
