using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Darbuotojas 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int id { get; set; }
        public Darbuotojas( string name, string surname, DateTime birth) 
        {
            Name = name;
            Surname = surname;
            Birth = birth;
        }
        public Darbuotojas(string name, string surname, DateTime birth, int ID)
        {
            Name = name;
            Surname = surname;
            Birth = birth;
            id = ID;
        }
        public Darbuotojas()
        {
        }
        }
}
