using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Account
    {
        public int ID { get; set; }
        public string Role { get; set; }
        public Account(int id, string role)
        {
            ID = id;
            Role = role;
        }
    }
}
