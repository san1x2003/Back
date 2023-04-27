using site.Models.Domain;

namespace site.Models
{
    public class UpdateClientViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
