﻿using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class InvoiceAuthorization
    {
        //public List<object> InvoiceProject { get; set; }
        public List<InvoiceFee> InvoiceFee { get; set; }
        //public List<object> InvoiceAdIA { get; set; }
    }
}