﻿using System;

namespace DALNetCore.Models
{
    public partial class Log
    {
        public long LogId { get; set; }
        public string LogMessage { get; set; }
        public DateTime? Created { get; set; }
    }
}
