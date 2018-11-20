using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017Day3
{
    class Program
    {
        static void Main(string[] args)
        {

            //int targetValue = 325489;
            //int targetValue = 12;
            //int targetValue = 22;

            var c = int.Parse(Console.ReadLine());


            while (true)
            {


                int total = 1;
                //int width = 3;
                int rise = 1;
                int run = 0;
                var manDist = 0;

                for (int width = 3; width < 100000; width += 2)
                {

                    var addToTotal = 2 * width + 2 * (width - 2);
                    if (total + addToTotal >= c)
                    {
                        var remainingAmount = c - total;
                        var mod = remainingAmount % rise;
                        var checkEvenOrOdd = (remainingAmount / rise) + 1;
                        //if (mod == 0)
                        //{
                        //    run = 0;
                        //}
                        //else 
                        if (checkEvenOrOdd % 2 == 1)
                        {//odd
                            run =  rise - mod;
                        }
                        else
                        {//even
                            run = mod;
                        }
                        manDist = rise + run;
                        break;
                    }
                    else
                    {
                        total += addToTotal;
                        rise++;
                    }
                }

                System.Console.WriteLine("Rise: {0} Run: {1} Man Distance: {2}", rise, run, manDist);
                c = int.Parse(Console.ReadLine());

                if(c == -999)
                {
                    break;
                }

            }
        }
    }
}
