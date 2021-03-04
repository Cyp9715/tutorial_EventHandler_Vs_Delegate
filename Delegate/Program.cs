using System;

namespace Delegate
{
    class CallbackArg { }

    class PrimeCallbackArg : CallbackArg
    {
        public int Prime;

        public PrimeCallbackArg(int prime)
        {
            this.Prime = prime;
        }
    }

    //소수 생성기.
    class PrimeGenerator
    {
        public delegate void PrimeDelegate(PrimeCallbackArg arg);

        PrimeDelegate callbacks;

        public void AddDelegate(PrimeDelegate callback)
        {
            callbacks += callback;
        }

        public void RemoveDelegate(PrimeDelegate callback)
        {
            callbacks -= callback;
        }

        public void Run(int limit)
        {
            for (int i = 2; i <= limit; ++i)
            {
                if(IsPrime(i) == true && callbacks != null)
                {
                    callbacks(new PrimeCallbackArg(i));
                }
            }
        }


        private bool IsPrime(int candidate)
        {
            if((candidate & 1) == 0)
            {
                return candidate == 2;
            }

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if (candidate % i == 0) return false;
            }

            return candidate != 1;

        }

    }



    class Program
    {
        // 콜백으로 등록될 메서드 1
        static void PrintPrime(PrimeCallbackArg arg)
        {
            Console.Write(arg.Prime + ", ");
        }

        static int Sum;

        // 콜백으로 등록될 메서드 2
        static void SumPrime(PrimeCallbackArg arg)
        {
            Sum += arg.Prime;
        }

        static void Main(string[] args)
        {
            PrimeGenerator gen = new PrimeGenerator();

            //PrintPrime 추가
            PrimeGenerator.PrimeDelegate callprint = PrintPrime;
            gen.AddDelegate(callprint);

            //SumPrime 추가
            PrimeGenerator.PrimeDelegate callsum = SumPrime;
            gen.AddDelegate(callsum);


            // if callbacks public.
            ////PrintPrime 추가
            //PrimeGenerator.PrimeDelegate callprint = PrintPrime;
            //gen.callbacks += callprint;


            ////SumPrime 추가
            //PrimeGenerator.PrimeDelegate callsum = SumPrime;
            //gen.callbacks += callsum;


            // 1 ~ 10 까지 소수를 구하고,
            gen.Run(10);
            Console.WriteLine();
            Console.WriteLine(Sum);

            // SumPrime 콜백 메서드를 제거한 후 다시 1 ~ 15 까지 소스를 구하는 메서드 호출
            gen.RemoveDelegate(callsum);
            gen.Run(15);
        }
    }
}
