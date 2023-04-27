namespace site.Models.Domain
{
    public class Client
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
