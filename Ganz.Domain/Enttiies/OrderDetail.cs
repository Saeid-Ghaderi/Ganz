namespace Ganz.Domain.Enttiies
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int Count { get; set; }
        public int OrderID { get; set; }
        public virtual required Order Order { get; set; }
    }
}