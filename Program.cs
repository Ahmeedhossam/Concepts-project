using System;
using System.Threading;

namespace GameOfLifeImperative
{
    class Program
    {
        // =========================================================
        // 1. Main Entry Point (نقطة البداية)
        // =========================================================
        static void Main(string[] args)
        {
            // إعدادات مساحة اللعبة
            int rows = 30;  // عدد الصفوف
            int cols = 60;  // عدد الأعمدة

            // إنشاء الشبكة
            bool[,] grid = new bool[rows, cols];

            // اختيار وضع البداية
            RandomGrid(grid);     // الوضع العشوائي
            //gliderpattern(grid); // وضع Glider للاختبار

            // إخفاء المؤشر لتحسين المظهر
            Console.CursorVisible = false;

            // حلقة اللعبة (Game Loop)
            while (true)
            {
                // إعادة المؤشر للأول
                Console.SetCursorPosition(0, 0);

                // 1. رسم الجيل الحالي
                PrintGrid(grid);

                // 2. حساب الجيل القادم (Imperative Logic + Higher Order)
                // التعديل: غيرنا الاسم هنا لـ Gamerules
                grid = GetNextGeneration(grid, Gamerules);

                // 3. انتظار لرؤية الحركة
                Thread.Sleep(200);
            }
        }

        // =========================================================
        // دالة القواعد (Higher Order Logic)
        // دي الدالة اللي بتتبعت كـ Parameter
        // =========================================================
        // التعديل: الاسم بقى Gamerules والمتغير بقى LiveorDead
        static bool Gamerules(bool LiveorDead, int neighbors)
        {
            if (LiveorDead)
            {
                // تعيش لو حواليها 2 أو 3، غير كدة تموت
                return (neighbors == 2 || neighbors == 3);
            }
            else
            {
                // تصحى لو حواليها 3 بالظبط
                return (neighbors == 3);
            }
        }

        // =========================================================
        // 2. Core Logic: Imperative Paradigm (المنطق الإلزامي)
        // =========================================================
        public static bool[,] GetNextGeneration(bool[,] currentGrid, Func<bool, int, bool> rule)
        {
            int rows = currentGrid.GetLength(0);
            int cols = currentGrid.GetLength(1);

            // مصفوفة جديدة عشان النتيجة (Mutable State Update Pattern)
            bool[,] newGrid = new bool[rows, cols];

            // Loop 1: التكرار على الصفوف
            for (int i = 0; i < rows; i++)
            {
                // Loop 2: التكرار على الأعمدة
                for (int j = 0; j < cols; j++)
                {
                    // حساب الجيران
                    int liveNeighbors = CountNeighbors(currentGrid, i, j);

                    // التعديل: غيرنا اسم المتغير هنا لـ LiveorDead
                    bool LiveorDead = currentGrid[i, j];

                    // تطبيق القواعد باستخدام الدالة المبعوتة
                    // التعديل: مررنا المتغير بالاسم الجديد
                    newGrid[i, j] = rule(LiveorDead, liveNeighbors);
                }
            }

            return newGrid;
        }

        // دالة مساعدة لعد الجيران المحيطين (8 خلايا)
        private static int CountNeighbors(bool[,] grid, int x, int y)
        {
            int count = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // تجاهل الخلية نفسها
                    if (i == 0 && j == 0) continue;

                    int newX = x + i;
                    int newY = y + j;

                    // التأكد إننا جوه حدود المصفوفة Check Boundaries
                    if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
                    {
                        if (grid[newX, newY])
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        // =========================================================
        // 3. Initialization Helpers (دوال التجهيز)
        // =========================================================

        // دالة التجهيز العشوائي
        static void RandomGrid(bool[,] grid)
        {
            Random rand = new Random();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // احتمال 20% تكون حية
                    if (rand.Next(5) == 0)
                    {
                        grid[i, j] = true;
                    }
                    else
                    {
                        grid[i, j] = false;
                    }
                }
            }
        }

        // دالة وضع Glider (للاختبار)
        static void gliderpattern(bool[,] grid)
        {
            if (grid.GetLength(0) > 5 && grid.GetLength(1) > 5)
            {
                grid[1, 2] = true;
                grid[2, 3] = true;
                grid[3, 1] = true;
                grid[3, 2] = true;
                grid[3, 3] = true;
            }
        }

        // =========================================================
        // 4. UI / Display Helpers (دوال العرض)
        // =========================================================
        static void PrintGrid(bool[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(grid[i, j] ? "O" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}