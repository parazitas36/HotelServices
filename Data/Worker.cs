using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Worker
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int ID { get; set; }
        public Worker(string name, string surname, DateTime birth, int id)
        {
            Name = name;
            Surname = surname;
            Birth = birth;
            ID = id;
        }
    }
}
