using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning
{
    public class Algorithm
    {
        int kNN;
        int totalTrainset;
        List<Data> trainset;
        List<Data> dataset;

        Distance[] distances;

        int maxAge;
        double maxDistance;


        public Algorithm(int k, List<Data> train, List<Data> data)
        {
            this.kNN = k;//k neighbor

            this.trainset = train;//trainset
            this.dataset = data;//data
            this.totalTrainset = train.Count;//total of data

            distances = new Distance[this.totalTrainset];

            //get max value of each column need to normalize
            MaximumValue maximum = new MaximumValue(train);
            maximum.findAllMax();

            maxAge = maximum.getMaxAge();
            maxDistance = maximum.getMaxDistance();


        }

        public void setResponse(Data data)
        {
            //normalize data
            Normalize ndata = new Normalize(data, maxAge, maxDistance);


            //calculate all distances
            for (int i = 0; i < this.totalTrainset; i++)
            {
                distances[i] = new Distance();
                distances[i].distance = 0;
                distances[i].index = i;

                //normalize element
                Normalize tmp = new Normalize(this.trainset[i], maxAge, maxDistance);


                //distance between two age normalized
                distances[i].distance = distances[i].distance + getDistance(ndata.age, tmp.age);

                //distance between two incoming normalized
                distances[i].distance = distances[i].distance + getDistance(ndata.distance, tmp.distance);

            }// end loop


            //sort
            for (int i = 0; i < totalTrainset - 1; i++)
            {
                for (int j = i + 1; j < totalTrainset; j++)
                {
                    if (distances[i].distance > distances[j].distance)
                    {
                        Distance tmp = distances[i];
                        distances[i] = distances[j];
                        distances[j] = tmp;
                    }//swap
                }//end j loop

            }//end i loop



            //select k nearest neighbor
            int yesCount = 0;
            int noCount = 0;

            for (int i = 0; i < kNN; i++)
            {
                Data tmp = trainset[distances[i].index];
                if (tmp.getResponse() == 0)
                {
                    noCount = noCount + 1;
                }
                else if (tmp.getResponse() == 1)
                {
                    yesCount = yesCount + 1;
                }
            }



            //set response value for unknown data
            if (yesCount > noCount)
            {
                data.setResponse(1);
            }
            else if (yesCount < noCount)
            {
                data.setResponse(0);
            }



        }

        public float getDistance(float a, float b)
        {
            return (a - b) * (a - b);
        }

        public void runkNN()
        {
            for (int i = 0; i < this.dataset.Count; i++)
            {
                setResponse(this.dataset[i]);
            }
        }

        public List<Data> getDataList()
        {
            return this.dataset;
        }


    }
}
