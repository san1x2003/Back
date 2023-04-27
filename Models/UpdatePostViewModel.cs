﻿using site.Models.Domain;

namespace site.Models
{
    public class UpdatePostViewModel
    {
        public Guid Id { get; set; }

        public string Identificator { get; set; }
        public int Poost { get; set; }
        public string TittlePost { get; set; }
        public LinkedList<Employee> Employee { get; set; } = new();
    }
}
