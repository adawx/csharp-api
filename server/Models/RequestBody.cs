using System;
using System.ComponentModel;

namespace server.Models
{
    public class RequestBody
    {
        public string Region { get; set; }
        [DefaultValue("2020-01-01")]
        public string? StartDate { get; set; }
        [DefaultValue("2021-12-31")]
        public string? EndDate { get; set; }
    }
}

