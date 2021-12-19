using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Room
    {
        public int Nr { get; set; }
        public string Status { get; set; }

        public Room(int nr, string status)
        {
            Nr = nr;
            Status = status;
        }
    }
}
