using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    public class Normalize
    {
        public float paramA;
        public float paramC;
        public float paramD;

        public Normalize(Data data, int maxA, int maxC, int maxD)
        {
            paramA = (float)data.getParamA() / (float)maxA;
            paramC = (float)data.getParamC() / (float)maxC;
            paramD = (float)data.getParamD() / (float)maxD;
        }
    }
}
