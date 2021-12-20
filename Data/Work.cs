using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Work
    {
        public string Name { get; set; }
        public int id { get; set; }
        public DateTime date { get; set; }
        public Work(int Id, string name)
        {
            Name = name;
            id = Id;
        }
        public Work( string name, DateTime Date)
        {
            Name = name;
            date = date;
        }
        public Work(int name, DateTime Date)
        {
            id = name;
            date = date;
        }
        public Work(int Id, string name, DateTime Date)
        {
            Name = name;
            id = Id;
            date = date;
        }
    }
}
