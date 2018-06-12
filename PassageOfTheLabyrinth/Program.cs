using System;

namespace PassageOfTheLabyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] array = new char[,]
            {
                { '.','.','.','.','#','.'},
                { '.','#','.','#','#','.'},
                { '.','.','#','.','.','#'},
                { '#','.','.','.','#','.'},
                { '.','.','#','.','.','.'}
            };

            Labyrinth labb = new Labyrinth(array);
            Console.WriteLine(
                labb.Start());

            Console.WriteLine(
                labb.GetPenetrable());
        }
    }
    class Labyrinth
    {
        char[,] labyrinth;
        bool IsPenetrable;

        public Labyrinth(char[,] labyrinth)
        {
            this.labyrinth = labyrinth;
        }

        /// <summary>
        /// Начинает прохождение лабиринта
        /// </summary>
        /// <returns></returns>
        public int Start()
        {
            return GoDown(0, 0);
        }

        public bool GetPenetrable()
        {
            GoDown(0, 0);
            return IsPenetrable;
        }

        //Методы перемещения по лабиринту с возвращением длины пути
        int GoDown(int x, int y, int length = 0)
        {
            if (IsReady(x, y))
                return length;
            else
            {
                if (length == labyrinth.Length)
                    return -1;

                int a = InTheArray(x + 1, y) ?
                        GoDown(x + 1, y, length + 1) : -1;
                int b = InTheArray(x, y + 1) ?
                        GoRight(x, y + 1, length + 1) : -1;
                int c = InTheArray(x, y - 1) ?
                        GoLeft(x, y - 1, length + 1) : -1;

                if (a > b && a > c) return a;
                if (b > a && b > c) return b;
                if (c > a && c > b) return c;

                return -1;
            }
        }

        int GoUp(int x, int y, int length)
        {
            if (IsReady(x, y))
                return length;
            else
            {
                if (length == labyrinth.Length)
                    return -1;

                int a = InTheArray(x, y + 1) ?
                        GoRight(x, y + 1, length + 1) : -1;
                int b = InTheArray(x, y - 1) ?
                        GoLeft(x, y - 1, length + 1) : -1;
                int c = InTheArray(x - 1, y) ?
                        GoUp(x - 1, y, length + 1) : -1;

                if (a > b && a > c) return a;
                if (b > a && b > c) return b;
                if (c > a && c > b) return c;

                return -1;
            }
        }

        int GoLeft(int x, int y, int length)
        {
            if (IsReady(x, y))
                return length;
            else
            {
                if (length == labyrinth.Length)
                    return -1;

                int a = InTheArray(x + 1, y) ?
                        GoDown(x + 1, y, length + 1) : -1;
                int b = InTheArray(x, y - 1) ?
                        GoLeft(x, y - 1, length + 1) : -1;
                int c = InTheArray(x - 1, y) ?
                        GoUp(x - 1, y, length + 1) : -1;

                if (a > b && a > c) return a;
                if (b > a && b > c) return b;
                if (c > a && c > b) return c;

                return -1;
            }
        }

        int GoRight(int x, int y, int length)
        {
            if (IsReady(x, y))
                return length;
            else
            {
                if (length == labyrinth.Length)
                    return -1;

                int a = InTheArray(x + 1, y) ?
                        GoDown(x + 1, y, length + 1) : -1;
                int b = InTheArray(x, y + 1) ?
                        GoRight(x, y + 1, length + 1) : -1;
                int c = InTheArray(x - 1, y) ?
                        GoUp(x - 1, y, length + 1) : -1;

                if (a > b && a > c) return a;
                if (b > a && b > c) return b;
                if (c > a && c > b) return c;

                return -1;
            }
        }

        //Проверяет корректность вызываемого элемента из массива
        bool InTheArray(int x, int y)
        {
            if (x >= 0 && y >= 0 &&
                x < labyrinth.GetLength(0) && y < labyrinth.GetLength(1) &&
                labyrinth[x, y] != '#')
                return true;
            else
                return false;
        }

        //Сообщает о достижении выхода из лабиринта
        bool IsReady(int x, int y)
        {
            if (x == labyrinth.GetLength(0) - 1 && y == labyrinth.GetLength(1) - 1)
            {
                IsPenetrable = true;
                return true;
            }
            else
                return false;
        }
    }
}
