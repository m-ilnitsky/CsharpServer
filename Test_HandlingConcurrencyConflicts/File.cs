namespace Test_HandlingConcurrencyConflicts
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
