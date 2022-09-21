using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Problem01
{
    class Program
    {
        static byte[] Data_Global = new byte[1000000000];
        static long Sum_Global = 0;
        static int threadCount = 24;
        static bool reading = false;

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
            reading = false;
            Span<byte> data = Data_Global;
            long sum_local = 0;
            for (int j = start; j < end; j++){
                if (data[j] % 2 == 0)
                {
                    sum_local -= data[j];
                }
                else if (data[j] % 3 == 0)
                {
                    sum_local += (data[j]*2);
                }
                else if (data[j] % 5 == 0)
                {
                    sum_local += (data[j] / 2);
                }
                else if (data[j] %7 == 0)
                {
                    sum_local += (data[j] / 3);
                }
                data[j] = 0;
            }
            Sum_Global += sum_local;
            // a++;   
            // return sum_local;
        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            /* Read data from file */
            Console.Write("Data read...");
            int y = ReadData();
            if (y == 0)
            {
                Console.WriteLine("Complete.");
            }
            else
            {
                Console.WriteLine("Read Failed!");
            }

            /* Start */
            Console.Write("\n\nWorking...");
            sw.Start();

            Thread[] threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++) {
                reading = true;
                int start = i * (1000000000 / threadCount);
                int end = (i + 1) * (1000000000 / threadCount);
                threads[i] = new Thread(() => sum(i, start, end));
                threads[i].Priority = ThreadPriority.Highest;
                threads[i].Start();
                // sleep to allow the thread to start
                Thread.Sleep(1);
                // while(reading){};
            }
            
            for (int i = 0; i < threadCount; i++) {
                threads[i].Join();
            }
            // Sum_Global = 888701676;
            sw.Stop();
            Console.WriteLine("Done.");

            /* Result */
            Console.WriteLine("Summation result: {0}", Sum_Global);
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
