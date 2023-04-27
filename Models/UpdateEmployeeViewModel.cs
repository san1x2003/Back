using site.Models.Domain;

namespace site.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public Guid PostId { get; set; }
        public Post? Post { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
