using System.Collections.Generic;

namespace Test_HandlingConcurrencyConflicts
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsIncoming { get; set; }

        public int ThreadId { get; set; }

        public virtual Thread Thread { get; set; }

        public virtual List<File> Files { get; set; } = new List<File>();
    }
}
