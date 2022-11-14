using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Text_Based_RPG_Map
{
    internal class Program
    {
        static char[,] map = new char[,] // dimensions defined by following data:
    {
        {'^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'^','^','`','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`'},
        {'^','^','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`','`','`'},
        {'^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`'},
        {'`','`','`','`','`','`','`','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
    };
        // map legend:
        // ^ = mountain
        // ` = grass
        // ~ = water
        // * = trees
        static void Main(string[] args)
        {
            int scale = 3;
            if (scale > 3)
                Console.SetWindowSize(Clamp(Clamp(scale, 1, 7) * 30 + 1, 31, 240), Console.WindowHeight);
            DisplayMap(scale);
            Console.ResetColor();
            Console.ReadLine();
        }

        static void DisplayMap()
        {
            int cols = map.GetLength(0);
            int rows = map.GetLength(1);
            //Console.WriteLine(cols);
            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    ChangeColour(map[col, row]);
                    Console.Write(map[col, row]);
                }

                Console.WriteLine();
            }
        }

        static void DisplayMap(int scale)
        {
            int cols = map.GetLength(0);
            int rows = map.GetLength(1);
            //Console.WriteLine(cols);
            for (int col = 0; col < cols; col++)
            {
                for (int scale_x = 0; scale_x < scale; scale_x++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        for (int scale_y = 0; scale_y < scale; scale_y++)
                        {
                            ChangeColour(map[col, row]);
                            Console.Write(map[col, row]);
                        }
                    }

                    Console.WriteLine();
                }

                //Console.WriteLine();
            }
        }

        static void ChangeColour(char character)
        {
            int[] mountain = {15, 7};
            int[] grass_and_trees = {10, 2};
            int[] water = {9, 1, 11, 3};

            var console_colours = Enum.GetValues(typeof(ConsoleColor));

            Random random = new Random();

            switch (character)
            {
                case '^':
                    Change(mountain);
                    break;

                case '`':
                    Change(grass_and_trees);
                    break;

                case '~':
                    Change(water);
                    break;

                case '*':
                    Change(grass_and_trees);
                    break;
            }

            void Change(int[] table)
            {
                List<int> lst = table.OfType<int>().ToList();

                (ConsoleColor result, int randomNumber) = ChooseRandom(lst);

                Console.ForegroundColor = result;

                lst.RemoveAt(randomNumber);

                (result, randomNumber) = ChooseRandom(lst);

                Console.BackgroundColor = result;
            }

            Tuple<ConsoleColor, int> ChooseRandom(List<int> lst)
            {
                int randomNumber = random.Next(lst.Count);

                return Tuple.Create((ConsoleColor)console_colours.GetValue(lst[randomNumber]), randomNumber);
            }
        }

        static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
