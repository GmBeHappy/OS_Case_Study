// using System;
// using System.Threading;
namespace experiment_1
{
    class Program
    {
        static int resource = 10000;
        static void TestThread1()
        {
            for (int i = 1; i <= 45555; i++)
            {
                resource++;
                Console.Write(".");
                if (i%100 == 0)
                {
                    Console.WriteLine();
                }
            }

        }
        static void Main(string[] args)
        {
            Thread thread_1 = new Thread(TestThread1);
            thread_1.Start();
            thread_1.Join();
            Console.WriteLine("resource = {0}", resource);
        }
    }
}