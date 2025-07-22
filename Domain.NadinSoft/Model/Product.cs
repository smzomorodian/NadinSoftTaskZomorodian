using Domain.NadinSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NadinSoft.Model
{
    public class Product
    {
        public Product() { }
        public Product(string name, DateTime produceDate, string manufacturePhone, string manufactureEmail, bool isAvailable, string userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            ProduceDate = produceDate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable = isAvailable;
            CreatedByUserId = userId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime ProduceDate { get; private set; }
        public string ManufacturePhone { get; private set;}
        public string ManufactureEmail { get; private set; }
        public bool IsAvailable { get; private set; }

        public string CreatedByUserId { get; private set; }
    }
}

