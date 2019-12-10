using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandPApis_GraphQL.Model
{
    public class AppointmentType
    {
        public string Id { get; set; }
        public EncounterType EncounterType { get; set; }
        public string Name { get; set; }

    }
}
