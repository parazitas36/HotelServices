using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Klientas
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime Birth { get; set; }
        public int id { get; set; }
        public Klientas(string name, string surname, string phone, DateTime birth, int id1)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            Birth = birth;
            id = id1;
        }
    }
}
