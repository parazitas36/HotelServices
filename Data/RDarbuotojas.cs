using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class RDarbuotojas : Account
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public RDarbuotojas(int id, string role, string name, string surname, DateTime birth) : base(id, role)
        {
            Name = name;
            Surname = surname;
            Birth = birth;
        }
    }
}
