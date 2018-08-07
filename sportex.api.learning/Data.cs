using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    public class Data
    {
        public int eventId; //event id
        public int age; //average age of participants
        public double distance; //distance to event
        public int response;

        public Data()
        {
            //do nothing
        }

        public void setEventId(int id)
        {
            this.eventId = id;
        }

        public int getEventId()
        {
            return this.eventId;
        }

        public void setAge(int pa)
        {
            this.age = pa;
        }

        public int getAge()
        {
            return this.age;
        }

        public void setDistance(double pc)
        {
            this.distance = pc;
        }

        public double getDistance()
        {
            return this.distance;
        }

        public void setResponse(int res)
        {
            this.response = res;
        }

        public int getResponse()
        {
            return this.response;
        }
    }
}
