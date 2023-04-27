namespace site.Models.Domain
{
    public class Sklad
    {
        public Guid Id { get; set; }

        public string NumberSklad { get; set; }

        public string Adress { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
