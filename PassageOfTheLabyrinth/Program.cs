using System;
using System.Collections.Generic;

namespace MyAlgoritm
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] array = new char[,]
            {
                { '.','.','.','.','.','.'},
                { '.','.','.','.','.','.'},
                { '.','.','.','.','.','.'},
                { '.','.','.','.','.','.'},
                { '.','.','.','.','.','.'},
            };

            Labyrinth lab = new Labyrinth(array);
            lab.Start(0, 0);

            Console.WriteLine(lab.GetLength());
            Console.WriteLine(lab.GetPenetrable());

        }
    }
    class Labyrinth
    {
        List<LabyrinthCell> list;
        char[,] labyrinth;
        int[,] WaweLabyrinth;
        bool IsPenetrable;
        bool IsStarted = false;

        public Labyrinth(char[,] labyrinth)
        {
            list = new List<LabyrinthCell>();
            this.labyrinth = labyrinth;
            WaweLabyrinth = new int[labyrinth.GetLength(0), labyrinth.GetLength(1)];

            //Стартовая инициализация волнового поля
            for (int x = 0; x < labyrinth.GetLength(1); x++)
            {
                for (int y = 0; y < labyrinth.GetLength(0); y++)
                {
                    if (labyrinth[y, x] == '#')
                        WaweLabyrinth[y, x] = -1;
                    else
                        WaweLabyrinth[y, x] = int.MaxValue;
                }
            }
        }

        /// <summary>
        /// Создает волновой алгоритм
        /// </summary>
        /// <param name="x">Координата X старта</param>
        /// <param name="y">Координата Y старта</param>
        /// <param name="length">Начальное значение длины пути, по умолчанию 0</param>
        /// <param name="start">true для распространения волны, по умолчанию true</param>
        public void Start(int x, int y, int length = 0, bool start = true)
        {
            if (GetCell(x, y) <= length)
                return;

            WaweLabyrinth[y, x] = length;

            if (GetCell(x, y + 1) != -1 && GetCell(x, y + 1) > length + 1)
                list.Add(new LabyrinthCell(x, y + 1, length + 1));
            if (GetCell(x + 1, y) != -1 && GetCell(x + 1, y) > length + 1)
                list.Add(new LabyrinthCell(x + 1, y, length + 1));
            if (GetCell(x, y - 1) != -1 && GetCell(x, y - 1) > length + 1)
                list.Add(new LabyrinthCell(x, y - 1, length + 1));
            if (GetCell(x - 1, y) != -1 && GetCell(x - 1, y) > length + 1)
                list.Add(new LabyrinthCell(x - 1, y, length + 1));

            if (start)
                while (list.Count != 0)
                {
                    Start(list[0].x, list[0].y, list[0].length, false);
                    list.RemoveAt(0);
                    if (GetCell(WaweLabyrinth.GetLength(1) - 1,
                        WaweLabyrinth.GetLength(0) - 1) != int.MaxValue
                        && GetCell(WaweLabyrinth.GetLength(1) - 1, 
                        WaweLabyrinth.GetLength(0) - 1) != -1)
                    {
                        IsPenetrable = true;
                        break;
                    }
                }
        }


        public bool GetPenetrable()
        {
            if (!IsStarted)
                Start(0, 0, 0, true);

            return IsPenetrable;
        }

        public int GetLength()
        {
            if (!IsStarted)
                Start(0, 0, 0, true);

            if (GetCell(WaweLabyrinth.GetLength(1) - 1,
                WaweLabyrinth.GetLength(0) - 1) == int.MaxValue)
                return -1;

            return GetCell(WaweLabyrinth.GetLength(1) - 1,
                   WaweLabyrinth.GetLength(0) - 1);
        }

        int GetCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < WaweLabyrinth.GetLength(1)
                && y < WaweLabyrinth.GetLength(0) && WaweLabyrinth[y, x] != -1)
                return WaweLabyrinth[y, x];

            return -1;
        }

        class LabyrinthCell
        {
            public readonly int x;
            public readonly int y;
            public readonly int length;

            public LabyrinthCell(int x, int y, int length)
            {
                this.x = x;
                this.y = y;
                this.length = length;
            }
        }
    }
}