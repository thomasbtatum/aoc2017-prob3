using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
--- Day 3: Spiral Memory ---
You come across an experimental new kind of memory stored on an infinite two-dimensional grid.

Each square on the grid is allocated in a spiral pattern starting at a location marked 1 and then counting up while spiraling outward. For example, the first few squares are allocated like this:

17  16  15  14  13
18   5   4   3  12
19   6   1   2  11
20   7   8   9  10
21  22  23---> ...
While this is very space-efficient (no squares are skipped), requested data must be carried back to square 1 (the location of the only access port for this memory system) by programs that can only move up, down, left, or right. They always take the shortest path: the Manhattan Distance between the location of the data and square 1.

For example:

Data from square 1 is carried 0 steps, since it's at the access port.
Data from square 12 is carried 3 steps, such as: down, left, left.
Data from square 23 is carried only 2 steps: up twice.
Data from square 1024 must be carried 31 steps.
How many steps are required to carry the data from the square identified in your puzzle input all the way to the access port?

Your puzzle answer was 552.
*/

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
