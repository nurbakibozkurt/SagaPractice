﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Service.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public decimal CardBalance { get; set; }
    }
}
