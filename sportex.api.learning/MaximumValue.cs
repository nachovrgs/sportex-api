using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    class MaximumValue
    {
        int maxAge = 0;
        int maxDistance = 0;

        List<Data> lData = new List<Data>();

        public MaximumValue(List<Data> ld)
        {
            this.lData = ld;
        }

        public void findAllMax()
        {
            for (int i = 0; i < this.lData.Count; i++)
            {
                //find max
                if (this.lData[i].getAge() > maxAge)
                {
                    maxAge = this.lData[i].getAge();
                }

                //find maxDistance
                if (this.lData[i].getDistance() > maxDistance)
                {
                    maxDistance = this.lData[i].getDistance();
                }

            }//end loop
        }


        public int getMaxAge()
        {
            return this.maxAge;
        }

        public int getMaxDistance()
        {
            return this.maxDistance;
        }

    }
}
