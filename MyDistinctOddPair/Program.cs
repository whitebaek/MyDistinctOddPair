using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDistinctOddPair
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 4, 2 };

            //Get distinct value 
            var distinctInput = input.Distinct<int>();

            MyClass obj = new MyClass();
            obj.Run(distinctInput.ToArray());

            Console.ReadLine();
        }
    }

    class MyClass
    {
        public void Run(int[] distinctInput)
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
    }
}
