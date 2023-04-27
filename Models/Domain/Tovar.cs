namespace site.Models.Domain
{
    public class Tovar
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
