// using System;
// using System.Threading;

namespace Lab_OS_Concurrency_02
{
    class Program
    {
        static int resource = 10000;
        static void TestThread1()
        {
            Console.WriteLine("Thread#1 i={0}", resource);
        }
        static void TestThread2()
        {
            Console.WriteLine("Thread#2 i={0}", resource);
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