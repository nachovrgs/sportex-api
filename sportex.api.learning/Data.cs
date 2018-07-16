using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    public class Data
    {
        private string name;
        private int paramA;
        private int paramB;
        private int paramC;
        private int paramD;
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

        public void setParamA(int pa)
        {
            this.paramA = pa;
        }

        public int getParamA()
        {
            return this.paramA;
        }


        public void setParamB(int pb)
        {
            this.paramB = pb;
        }

        public int getParamB()
        {
            return this.paramB;
        }


        public void setParamC(int pc)
        {
            this.paramC = pc;
        }

        public int getParamC()
        {
            return this.paramC;
        }

        public void setParamD(int pd)
        {
            this.paramD = pd;
        }

        public int getParamD()
        {
            return this.paramD;
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
