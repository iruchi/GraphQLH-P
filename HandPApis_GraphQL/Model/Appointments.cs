using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandPApis_GraphQL.Model
{
    public class Appointment
    {
        public DateTime? Start { get; set; }
        public DateTime? InsertedAt { get; set; }
        public AppointmentType Type { get; set; }
        public string Status { get; set; }
        public Location Location { get; set; }
        public Provider Provider { get; set; }
        public Patient Patient { get; set; }
    }
}
