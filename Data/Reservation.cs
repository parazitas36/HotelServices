using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Reservation
    {
        public int ID { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string Status { get; set; }
        public int RoomNumber { get; set; }

        public Reservation(int id, DateTime starts, DateTime ends, string status, int roomNumber)
        {
            ID = id;
            Starts = starts;
            Ends = ends;
            Status = status;
            RoomNumber = roomNumber;
        }

        public Reservation()
        {
        }
    }
}
