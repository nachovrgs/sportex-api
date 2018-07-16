using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    class MaximumValue
    {
        int maxA = 0;
        int maxC = 0;
        int maxD = 0;

        List<Data> lData = new List<Data>();

        public MaximumValue(List<Data> ld)
        {
            this.lData = ld;
        }

        public void findAllMax()
        {
            for (int i = 0; i < this.lData.Count; i++)
            {
                //find maxA
                if (this.lData[i].getParamA() > maxA)
                {
                    maxA = this.lData[i].getParamA();
                }

                //find maxC
                if (this.lData[i].getParamC() > maxC)
                {
                    maxC = this.lData[i].getParamC();
                }


                //find maxNumcard
                if (this.lData[i].getParamD() > maxD)
                {
                    maxD = this.lData[i].getParamD();
                }

            }//end loop
        }


        public int getMaxA()
        {
            return this.maxA;
        }

        public int getMaxC()
        {
            return this.maxC;
        }

        public int getMaxD()
        {
            return this.maxD;
        }
    }
}
