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


            //Part1();
            Part2();


        }


        static void Part1()
        {


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
                            run = rise - mod;
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

                if (c == -999)
                {
                    break;
                }

            }
        }

        
        public enum Wall
        {

            NorthWall = 0,
            WestWall  = 1,
            SouthWall = 2,
            EastWall  = 3 

        }

        static Wall NextWall(Wall wall)
        {
            var nextWall = Convert.ToInt32(wall) + 1;
            if(nextWall == 4)
            {
                return Wall.NorthWall;
            }
            return (Wall)nextWall;
        }

        static void Part2()
        {
            var orderedWalls = Enum.GetValues(typeof(Wall));


            /*
            As a stress test on the system, the programs here clear the grid and then store the value 1 in square 1. 
            Then, in the same allocation order as shown above, they store the sum of the values in all adjacent squares, including diagonals.

            So, the first few squares' values are chosen as follows:

            Square 1 starts with the value 1.
            Square 2 has only one adjacent filled square (with value 1), so it also stores 1.
            Square 3 has both of the above squares as neighbors and stores the sum of their values, 2.
            Square 4 has all three of the aforementioned squares as neighbors and stores the sum of their values, 4.
            Square 5 only has the first and fourth squares as neighbors, so it gets the value 5.
            Once a square is written, its value does not change. Therefore, the first few squares would receive the following values:

            147  142  133  122   59
            304    5    4    2   57
            330   10    1    1   54
            351   11   23   25   26
            362  747  806--->   ...
            What is the first value written that is larger than your puzzle input?

            Your puzzle input is still 325489.
            */

            //initialize each with the first entry
            List<List<int>>[] Walls = new List<List<int>>[4];
            foreach (var wallSide in orderedWalls)
            {
                var q = new List<int>() { 1 };
                Walls[Convert.ToInt32(wallSide)] = new List<List<int>>();
                Walls[Convert.ToInt32(wallSide)].Add(q);
            }

            //add another 1 to both north and south
            Walls[Convert.ToInt32(Wall.NorthWall)][0].Add(1);
            Walls[Convert.ToInt32(Wall.SouthWall)][0].Add(1);
            Walls[Convert.ToInt32(Wall.EastWall)][0].Add(2);
            Walls[Convert.ToInt32(Wall.NorthWall)].Add(new List<int> { 2 });

            //WE are starting to go westward on the north wall.  Wall size is 2.
            int wallSize = 3;

            while (true)
            {
                int count = 1;
                int wallPosition = 1;
                int lastValue = 2;

                foreach (var wallSide in orderedWalls)
                {
                    var currentWall = Walls[Convert.ToInt32(wallSide)].Last();
                    var nextWall = Walls[Convert.ToInt32(NextWall((Wall)wallSide))].Last();

                    var startPosition = currentWall.Count;
                    //loop used to add things up
                    for (int wallWalk = startPosition; wallWalk < wallSize; wallWalk++)
                    {
                        int total = wallPosition + 50;//always add in the last value = 50 is placeholder

                        //in last position
                        if (wallWalk == wallSize-1)//we are at the corner
                        {
                            //once we get total, add to current
                            //create a new one on the next wall with just the total
                            currentWall.Add(total);
                            Walls[Convert.ToInt32(NextWall((Wall)wallSide))].Add(new List<int> { total });
                        } else
                        {
                            //next to last position
                            if(wallWalk == wallSize-2)
                            {
                                //once we get total, add to current
                                //push it to the beginning of the next wall
                                currentWall.Add(total);
                                nextWall.Insert(0,total);
                            }
                            else //normal position
                            {//else we take as many as we can - max of 3 off the wall and one trailer


                                currentWall.Add(total);
                            }
                        }
                        wallPosition++;

                    }

                    int theIndex = Walls[Convert.ToInt32(wallSide)].Count - 2;

                    Console.WriteLine(string.Format("Wall: {0} Size: {1} Wall: {2} Last Wall: {3}", wallSide, wallSize, String.Join(", ", currentWall.ToArray()), String.Join(", ", Walls[Convert.ToInt32(wallSide)][theIndex].ToArray())));

                    if(count > 1 && Convert.ToInt32(wallSide) % 2 == 1) {
                        wallSize++;
                    }

                    count++;
                }

                Console.ReadKey();
            }


        }


    }
}
