﻿using System.Text.Json;

namespace backendTask.DBContext.Models
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}
