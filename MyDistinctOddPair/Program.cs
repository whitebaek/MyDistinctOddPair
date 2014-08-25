using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDistinctOddPair
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] input = { 1, 2, 3, 4, 2 };
            int size = 99;
            int[] input = new int[size];
            for (int i = 0; i < size; i++)
            {
                input[i] = i;
            }

            //Get distinct value 
            var distinctInput = input.Distinct<int>();
            MyClass obj = new MyClass();

            Stopwatch swThread = new Stopwatch();
            swThread.Start();
            obj.RunThread(distinctInput.ToArray());
            swThread.Stop();

            Stopwatch swParallel = new Stopwatch();
            swParallel.Start();
            obj.RunParallel(distinctInput.ToArray());
            swParallel.Stop();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            obj.Run(distinctInput.ToArray());
            sw.Stop();

            Console.WriteLine("Thread Elapsed={0} : Parallel Elapsed={1} : Elapsed={2}", swThread.Elapsed, swParallel.Elapsed, sw.Elapsed);


            Console.ReadLine();
        }
    }

    class MyClass
    {
        public void RunThread(int[] distinctInput)
        {
            int a, b; 
            // 10개의 쓰레드가 동일 메서드 실행
            for (int i = 0; i < distinctInput.Length; i++)
            {
                //new Thread(UnsafeCalc).Start();
                for (int j = 0; j < distinctInput.Length; j++)
                {
                    if (i < j && (distinctInput[i] + distinctInput[j]) % 2 != 0)
                    {
                        a = distinctInput[i];
                        b = distinctInput[j];
                        Thread t3 = new Thread(() => SafeCalc(a, b));
                        t3.Start();
                    }
                }
                
            }
        }

        public void RunParallel(int[] distinctInput)
        {
            Parallel.For(0, distinctInput.Length, (i) =>
            {
                Parallel.For(0, distinctInput.Length, (j) =>
                {
                    if (i < j && (distinctInput[i] + distinctInput[j]) % 2 != 0)
                    {
                        Console.WriteLine("{0}+{1}", distinctInput[i], distinctInput[j]);
                    }
                });
            });

        }

        public void Run(int[] distinctInput)
        {
            int a, b;
            // 10개의 쓰레드가 동일 메서드 실행
            for (int i = 0; i < distinctInput.Length; i++)
            {
                //new Thread(UnsafeCalc).Start();
                for (int j = 0; j < distinctInput.Length; j++)
                {
                    if (i < j && (distinctInput[i] + distinctInput[j]) % 2 != 0)
                    {
                        a = distinctInput[i];
                        b = distinctInput[j];
                        SafeCalc(distinctInput[i], distinctInput[j]);
                    }
                }

            }
        }


        // lock문에 사용될 객체
        private object lockObject = new object();

        // Thread-Safe 메서드 
        private void SafeCalc(int i, int j)
        {
            //// 한번에 한 쓰레드만 lock블럭 실행
            //lock (lockObject)
            //{
            //    // 필드값 읽기
            //    //Console.WriteLine(counter);
            //    Console.WriteLine("{0}+{1}",i,j);
            //}
            
            Console.WriteLine("{0}+{1}", i, j);

        }
        //public void Run(int[] distinctInput)
        //{

        //    //Parallel.For(0, distinctInput.Length, (i) =>
        //    //{
        //    //    Parallel.For(0, distinctInput.Length, (j) =>
        //    //    {
        //    //        if (i < j && (distinctInput[i] + distinctInput[j]) % 2 != 0)
        //    //        {
        //    //            Console.WriteLine("{0}+{1}", distinctInput[i], distinctInput[j]);
        //    //        }
        //    //    });
        //    //});
        //}
    }
}
