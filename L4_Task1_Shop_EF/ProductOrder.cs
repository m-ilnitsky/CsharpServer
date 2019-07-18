namespace L4_Task1_Shop_EF
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
