﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Models.Responses
{
    public class AccountLookupResponse : Response
    {
        public Account Account { get; set; }
    }
}
