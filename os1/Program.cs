// using System;
// using System.Threading;
namespace experiment_1
{
    class Program
    {
        static void TestThread1()
        {
            int i;
            for (i = 0; i < 100000; i++)
            {
                Console.WriteLine("1", i);
            }
        }
        static void TestThread2()
        {
            int i;
            for (i = 0; i < 100000; i++)
            {
                Console.WriteLine("2",i);
            }
        }
        static void Main(string[] args)
        {
            Thread thread_1 = new Thread(TestThread1);
            Thread thread_2 = new Thread(TestThread2);
            thread_1.Start();
            thread_2.Start();
        }
    }
}