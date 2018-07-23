using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportex.api.learning.Helpers
{
    public static class DataHelper
    {

        public static double[][] LoadData(string filePath)
        {
            TextReader readFile = new StreamReader(filePath);
            var csv = new CsvReader(readFile);
            var records = csv.GetRecords<Item>();
            double[][] data = new double[1000][];

            int i = 0;
            foreach (var row in records)
            {
                data[i] = new double[] { row.distance, row.age, row.result };
                i++;
            }
            return data;
        }
    }
}
