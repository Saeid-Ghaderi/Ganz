namespace Ganz.Domain.Enttiies
{
    public class Order
    {
        public int ID { get; set; }
        public string? OrderNumber { get; set; }
        public virtual required ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
