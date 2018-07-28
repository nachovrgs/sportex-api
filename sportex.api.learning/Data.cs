using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    public class Data
    {
        private string name; //event name
        private int age; //average age of participants
        private int distance; //distance to event
        private int response;

        public Data()
        {
            //do nothing
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public void setAge(int pa)
        {
            this.age = pa;
        }

        public int getAge()
        {
            return this.age;
        }

        public void setDistance(int pc)
        {
            this.distance = pc;
        }

        public int getDistance()
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
