﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Data
{
    public class Kambarys
    {
        public int Nr { get; set; }
        public string Statusas { get; set; }

        public Kambarys(int nr, string status)
        {
            this.Nr = nr;
            this.Statusas = status;
        }

        public Kambarys()
        {
        }
    }
}
