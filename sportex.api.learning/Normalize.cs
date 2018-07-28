using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    public class Normalize
    {
        public float age;
        public float distance;

        public Normalize(Data data, int maxA, int maxC)
        {
            age = (float)data.getAge() / (float)maxA;
            distance = (float)data.getDistance() / (float)maxC;
        }
    }
}
