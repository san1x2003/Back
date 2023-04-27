using site.Models.Domain;

namespace site.Models
{
    public class AddClientViewModel
    {

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
