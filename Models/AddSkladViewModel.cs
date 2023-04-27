using site.Models.Domain;

namespace site.Models
{
    public class AddSkladViewModel
    {
       

            public string NumberSklad { get; set; }

            public string Adress { get; set; }
            public LinkedList<Zakaz> Zakaz { get; set; } = new();
        
    }

}

