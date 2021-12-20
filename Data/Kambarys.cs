using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Kambarys
    {
        public int Nr { get; set; }
        public int Statusas { get; set; }

        public Kambarys(int nr, int surname)
        {
            Nr = nr;
            Statusas = surname;
        }
    }

}
