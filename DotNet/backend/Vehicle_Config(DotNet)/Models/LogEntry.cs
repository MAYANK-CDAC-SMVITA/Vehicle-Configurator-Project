using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Vehicle_Config_DotNet_.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
