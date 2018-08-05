using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.learning.Helpers
{
    public sealed class DataMap : ClassMap<Data>
    {
        public DataMap()
        {
            Map(m => m.eventId);
            Map(m => m.age);
            Map(m => m.distance);
            Map(m => m.response);
        }
    }
}
