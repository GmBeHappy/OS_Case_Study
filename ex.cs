using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
// use vector instead of array
using System.Numerics;

namespace Problem01
{
    class Program
    {
        static byte[] Data_Global = new byte[1000000000];
        // create int array size 8
        static int[] sum_global = new int[8];
        static long finalSum = 0;
        static int G_index = 0;
        static int threadCount = 8;

        static int ReadData()
        {
            int returnData = 0;
            FileStream fs = new FileStream("Problem01.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            try 
            {
                Data_Global = (byte[]) bf.Deserialize(fs);
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Read Failed:" + se.Message);
                returnData = 1;
            }
            finally
            {
                fs.Close();
            }

            return returnData;
        }
        static void sum(int id, int start, int end)
        {
            int sum_local = 0;
            // split the array into threadCount parts
            for (int j = start; j < end; j++){
                if (Data_Global[j] % 2 == 0)
                {
                    sum_local -= Data_Global[j];
                }
                else if (Data_Global[j] % 3 == 0)
                {
                    sum_local += (Data_Global[j]*2);
                }
                else if (Data_Global[j] % 5 == 0)
                {
                    sum_local += (Data_Global[j] / 2);
                }
                else if (Data_Global[j] %7 == 0)
                {
                    sum_local += (Data_Global[j] / 3);
                }
                Data_Global[j] = 0;
            }
            // store the sum_local into sum_global
            sum_global[id] += sum_local;
        }

        // faster than sum
        

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int i, y;
            Console.Write("Data read...");
            y = ReadData();
            if (y == 0)
            {
                Console.WriteLine("Complete.");
            }
            else
            {
                Console.WriteLine("Read Failed!");
            }
            Console.Write("\n\nWorking...");
            sw.Start();
            Thread[] threads = new Thread[threadCount];
            for (i = 0; i < threadCount; i++) {
                int start = i * (1000000000 / threadCount);
                int end = (i + 1) * (1000000000 / threadCount);
                // Console.WriteLine("start: " + start + " end: " + end);
                threads[i] = new Thread(() => sum(i, start, end));
                threads[i].Start();
                // Console.WriteLine("Launching Thread {0}", i);
            }
            for (i = 0; i < threadCount; i++) {
                threads[i].Join();
            }
            finalSum = sum_global.Sum();
            sw.Stop();
            Console.WriteLine("Done.");
            Console.WriteLine("Summation result: {0}", finalSum);
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
