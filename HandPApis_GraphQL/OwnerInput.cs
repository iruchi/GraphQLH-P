using HandPApis_GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandPApis_GraphQL
{
    public class OwnerInput
    {
        public DateTime Start { get; set; }
        public DateTime InsertedAt { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
