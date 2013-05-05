using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InterPrep
{
    public class LockEx
    {
        static int maximumThreads = 3;
        static object locker = new object();

        static void WaitOne()
        {
            while (true)
            {
                lock (locker)
                {
                    if (maximumThreads > 0)
                    {
                        maximumThreads--;
                        break;
                    }
                }
            }
        }

        static void Release()
        {
            lock (locker)
            {
                maximumThreads++;
            }
        }

        public static void Worker()
        {
            WaitOne();

            Console.WriteLine("{0} Enters", Thread.CurrentThread.Name);
            Thread.Sleep(3000);
            Console.WriteLine("{0} Exists", Thread.CurrentThread.Name);

            Release();
        }
    }

    // How to fix? Use one lock object in all cases
    // or perform a detailed analysis of the lock usage throughout the program and choose a more appropriate locking scheme
    public class DeadlockEx
    {
        static object A = new object();
        static object B = new object();

        public static void MethodA()
        {
            Console.WriteLine("Inside methodA");
            lock (A)
            {
                Console.WriteLine("MethodA: Inside LockA and Trying to enter LockB");
                Thread.Sleep(5000);
                lock (B)
                {
                    Console.WriteLine("MethodA: inside LockA and inside LockB");
                    Thread.Sleep(5000);
                }
                Console.WriteLine("MethodA: inside LockA and outside LockB");
            }
            Console.WriteLine("MethodA: outside LockA and outside LockB");
        }

        public static void MethodB()
        {
            Console.WriteLine("Inside methodB");
            lock (B)
            {
                Console.WriteLine("methodB: Inside LockB");
                Thread.Sleep(5000);
                lock (A)
                {
                    Console.WriteLine("methodB: inside LockB and inside LockA");
                    Thread.Sleep(5000);
                }
                Console.WriteLine("methodB: inside LockB and outside LockA");
            }
            Console.WriteLine("methodB: outside LockB and outside LockA");
        }
    }
    
    class Program
    {

        static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                Thread thread = new Thread(LockEx.Worker);
                thread.Name = String.Concat("Thread ", i + 1);
                thread.Start();
            }

            Thread Thread1 = new Thread(DeadlockEx.MethodA);
            Thread Thread2 = new Thread(DeadlockEx.MethodB);
            Thread1.Start();
            Thread2.Start();
            Console.WriteLine("enter.....");
            Console.ReadLine();
        }
    }
}