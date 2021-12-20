using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Ataskaita
    {
        public DateTime date { get; set; }
        public DateTime Surname { get; set; }
        public DateTime Birth { get; set; }
        public int id { get; set; }
        public Ataskaita(DateTime Date,DateTime surname, DateTime birth)
        {
            date = Date;
            Surname = surname;
            Birth = birth;
        }
    }
}
