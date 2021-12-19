using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class RoomState
    {
        public int Id { get; set; }
        public string name { get; set; }

        public RoomState(int id, string name1)
        {
            Id = id;
            name = name1;
        }
    }

}
