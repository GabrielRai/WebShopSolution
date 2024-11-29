namespace Repository.Classes
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductRequest> Products { get; set; }
    }
}
