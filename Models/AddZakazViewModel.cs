﻿using site.Models.Domain;

namespace site.Models
{
    public class AddZakazViewModel
    {

        public Guid? ClientID { get; set; }
        public Client? Client { get; set; }

        public Guid? SkladId { get; set; }
        public Sklad? Sklad { get; set; }

        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Guid? TovarId { get; set; }
        public Tovar? Tovar { get; set; }

        public string NumberContact { get; set; }
        public DateTime Data { get; set; }
        public string Adress { get; set; }


        public DateTime? OrderDate { get; set; }
    }
}
