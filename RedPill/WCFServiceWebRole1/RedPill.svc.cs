using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RedPill : IRedPill
    {



        public Guid WhatIsYourToken()
        {
            return Guid.Parse("xxx");
        }


        [FaultContractAttribute(typeof(System.ArgumentOutOfRangeException))]
        public long FibonacciNumber(long n)
        {
            if (n > 92) throw new ArgumentOutOfRangeException("n", "Fib(>92) will cause a 64-bit integer overflow.");

            return FasterFibonacci(n);
        }


        /**
             * basd on the fact
             * F(2k) = F(k)[2F(k+1) – F(k)]
             * F(2k+1) = F(k+1)2 + F(k)2
             * proof can be found at http://www.m-hikari.com/imf-password2008/1-4-2008/ulutasIMF1-4-2008.pdf
             * **/
        private long FasterFibonacci(long n)
        {
            if (n == 0) return 0;
            if (n <= 2)
                return 1;
            long k = n / 2;
            long a = FasterFibonacci(k + 1);
            long b = FasterFibonacci(k);

            if (n % 2 == 1)
                return a * a + b * b;
            else
                return b * (2 * a - b);
        }



        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            if (a + b > c && b + c > a && a + c > b)
            {
                if (a == b && b == c)
                {
                    return TriangleType.Equilateral;
                }
                else if (a == b || b == c || c == a)
                {
                    return TriangleType.Isosceles;
                }else
                {
                    return TriangleType.Scalene;
                }

            }
            else
            {
                return TriangleType.Error;
            }
        }


        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentNullException))]
        public string ReverseWords(string s)
        {
            if (s == null) throw new ArgumentNullException("s", "Value cannot be null.");

            var textOriginal = s;
            string temp = textOriginal;

            int i = 0;
            while (temp.EndsWith("\r\n"))
            {
                temp = temp.Remove(temp.Length - 2);
                i++;
            }
            //i is how many \r\n in the last part of string.

            string[] lines = Regex.Split(textOriginal, "\r\n|\r|\n");

            StringBuilder builder = new StringBuilder();
            foreach (var line in lines)
            {
                var reversedWords = string.Join(" ", line.Split(' ').Select(x => new String(x.Reverse().ToArray())));
                builder.Append(string.Concat(reversedWords));
                builder.Append("\r\n");
            }
            var bigResult = builder.ToString();

            bigResult = bigResult.TrimEnd("\r\n".ToArray());
            for (int j = 0; j < i; j++)
            {
                bigResult = bigResult + "\r\n";
            }
            return bigResult;
        }


        
    }
}
