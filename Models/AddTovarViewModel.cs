﻿using site.Models.Domain;

namespace site.Models
{
    public class AddTovarViewModel
    {


        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
        public LinkedList<Zakaz> Zakaz { get; set; } = new();
    }
}
