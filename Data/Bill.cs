using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Bill
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Decimal Sum { get; set; }
        public string Status { get; set; }
        public int RegWorkerId { get; set; }
        public int ReservationId { get; set; }
        public int CliendId { get; set; }

        public Bill(int id, DateTime date, Decimal sum, string status, int rgwid, int rid, int cid)
        {
            ID = id;
            Date = date;
            Sum = sum;
            Status = status;
            RegWorkerId = rgwid;
            ReservationId = rid;
            CliendId = cid;
        }
    }
}
