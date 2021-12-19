using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class BillClients
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
        public int RegWorkerId { get; set; }
        public int ReservationId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurName { get; set; }

        public BillClients(int id, DateTime date, decimal sum, string status, int rgwid, int rid, string cname, string csname)
        {
            ID = id;
            Date = date;
            Sum = sum;
            Status = status;
            RegWorkerId = rgwid;
            ReservationId = rid;
            ClientName = cname;
            ClientSurName = csname;
        }
        public BillClients()
        {

        }
    }
}
