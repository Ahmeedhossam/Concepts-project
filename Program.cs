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

            // اختيار وضع البداية (فك التعليق عن الوضع الذي تريده)

            // الخيار 1: الوضع العشوائي (ممتع للمشاهدة)
            InitializeRandomGrid(grid);

            // الخيار 2: وضع Glider (جيد للاختبار والتأكد من صحة القواعد)
            //InitializeGlider(grid);

            // إخفاء المؤشر لتحسين المظهر
            Console.CursorVisible = false;

            // حلقة اللعبة (Game Loop)
            while (true)
            {
                // إعادة المؤشر للأول (أفضل من Clear لتقليل الرعشة)
                Console.SetCursorPosition(0, 0);

                // 1. رسم الجيل الحالي
                Console.Clear();
                PrintGrid(grid);

                // 2. حساب الجيل القادم (Imperative Logic)
                grid = GetNextGeneration(grid);

                // 3. انتظار لرؤية الحركة
                Thread.Sleep(800);
            }
        }

        // =========================================================
        // 2. Core Logic: Imperative Paradigm (المنطق الإلزامي)
        // =========================================================
        public static bool[,] GetNextGeneration(bool[,] currentGrid)
        {
            int rows = currentGrid.GetLength(0);
            int cols = currentGrid.GetLength(1);

            // مصفوفة جديدة عشان النتيجة (عشان مغيرش في الحالية وأنا بحسب)
            bool[,] newGrid = new bool[rows, cols];

            // Loop 1: التكرار على الصفوف
            for (int i = 0; i < rows; i++)
            {
                // Loop 2: التكرار على الأعمدة
                for (int j = 0; j < cols; j++)
                {
                    // حساب الجيران
                    int liveNeighbors = CountNeighbors(currentGrid, i, j);
                    bool isAlive = currentGrid[i, j];

                    // تطبيق القواعد الأربعة بـ IF/ELSE Statements
                    if (isAlive)
                    {
                        // قاعدة الوحدة (أقل من 2) أو الزحمة (أكتر من 3) -> تموت
                        if (liveNeighbors < 2 || liveNeighbors > 3)
                        {
                            newGrid[i, j] = false;
                        }
                        // قاعدة البقاء (2 أو 3) -> تعيش
                        else
                        {
                            newGrid[i, j] = true;
                        }
                    }
                    else
                    {
                        // قاعدة التكاثر (بالضبط 3) -> تصحى
                        if (liveNeighbors == 3)
                        {
                            newGrid[i, j] = true;
                        }
                    }
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

        // دالة التجهيز العشوائي (بنسبة 20%)
        static void InitializeRandomGrid(bool[,] grid)
        {
            Random rand = new Random();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // رقم عشوائي من 0 لـ 4 (5 احتمالات)
                    // لو طلع 0 (احتمال 20%) تبقى حية
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
        static void InitializeGlider(bool[,] grid)
        {
            // تأكد إن المصفوفة كبيرة كفاية قبل وضع القيم
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
                    // لو حية ارسم O لو ميتة ارسم نقطة .
                    Console.Write(grid[i, j] ? "O" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}