using System;


namespace HandPApis_GraphQL.Model
{
    public class Class1
    {
        public Class1 (DateTime d, AppointmentType a, Location l)
        {
            d = Start;
            a = Type;
            l = Location;
            
        }
        public DateTime Start { get; set; }
        public AppointmentType Type { get; set; }
        public Location Location { get; set; }
    }
}
