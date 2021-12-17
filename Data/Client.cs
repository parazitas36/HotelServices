using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Client : Account
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime Birth { get; set; }
        public Client(int id, string role, string name, string surname, string phone, DateTime birth) : base(id, role)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            Birth = birth;
        }
    }
}
