using CsvHelper;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace convertCSV
{
    public class Data
    {
        public Data(int delta, int theta, int lowAlpha, int highAlpha, int lowBeta, int highBeta, int lowGamma, int highGamma, string label)
        {
            this.delta = delta;
            this.theta = theta;
            this.lowAlpha = lowAlpha;
            this.highAlpha = highAlpha;
            this.lowBeta = lowBeta;
            this.highBeta = highBeta;
            this.lowGamma = lowGamma;
            this.highGamma = highGamma;
            this.label = label;
        }

        public int delta { get; set; }
        public int theta { get; set; }
        public int lowAlpha { get; set; }
        public int highAlpha { get; set; }
        public int lowBeta { get; set; }
        public int highBeta { get; set; }
        public int lowGamma { get; set; }
        public int highGamma { get; set; }
        public string label { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string path = null;
            Console.WriteLine("Enter folder path : ");
            path = Console.ReadLine();
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(path);
            List<Data> data = new List<Data>();

            foreach (string fileName in fileEntries)
                ProcessFile(fileName, data);

            using (var writer = new StreamWriter(path + "\\finalVersion.csv"))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteHeader<Data>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(data);

                writer.Flush();
            }
        }

        public static void ProcessFile(string path, List<Data> data)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string label = "None";
                if (path.Contains("Radosc"))
                {
                    label = "Joy";
                }
                else if (path.Contains("Relaks"))
                {
                    label = "Relax";
                }
                else if (path.Contains("Smutek"))
                {
                    label = "Sadness";
                }
                else if (path.Contains("Strach"))
                {
                    label = "Fear";
                }
                int[,] result = new int[16, 10];
                string currentLine;
                int currentLineNumber = 0;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (currentLineNumber > 10 && currentLineNumber < 26)
                    {
                        string[] subs = currentLine.Split(';');
                        for (int i = 0; i < subs.Length; i++)
                        {
                            result[currentLineNumber - 11, i] += Int32.Parse(subs[i]);
                        }
                    }
                    else if (currentLineNumber >= 26)
                    {
                        string[] subs = currentLine.Split(';');
                        for (int i = 0; i < subs.Length; i++)
                        {
                            result[15, i] += Int32.Parse(subs[i]);
                        }
                        Double[] averageResultDouble = new Double[10];
                        for (int i = 0; i < 16; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                averageResultDouble[j] += result[i, j];
                                if (i == 15)
                                {
                                    result[i, j] = 0;
                                }
                                else
                                {
                                    result[i, j] = result[i + 1, j];
                                }

                            }
                        }
                        int[] averageResultInt = new int[10];
                        for (int i = 0; i < 10; i++)
                        {
                            averageResultInt[i] = (int)Math.Round(averageResultDouble[i] / 16);
                        }
                        data.Add(new Data(averageResultInt[2], averageResultInt[3], averageResultInt[4], averageResultInt[5], averageResultInt[6], averageResultInt[7], averageResultInt[8], averageResultInt[9], label));
                    
                    }
                    currentLineNumber++;
                }
            }
        }
    }

}


