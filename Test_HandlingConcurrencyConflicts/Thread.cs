using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_HandlingConcurrencyConflicts
{
    public class Thread
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Subject { get; set; }

        [ConcurrencyCheck]
        public int Status { get; set; }

        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
