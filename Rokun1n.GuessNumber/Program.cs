using System;
using System.Collections.Generic;

namespace Rokun1n.GuessNumber
{
    internal class Program
    {
        private const int MinNumber = 1;
        private const int MaxNumber = 1000;
        private const int MaxAttempts = 10;

        static void Main()
        {
            List<int> gameResults = new();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== Гра \"Вгадай число\" ===");
                Console.WriteLine("1. Нова гра");
                Console.WriteLine("2. Показати статистику за сеанс");
                Console.WriteLine("0. Вихід");

                int choice = ReadInt("Ваш вибір: ", 0, 2);

                isRunning = choice switch
                {
                    1 => HandleNewGame(gameResults),
                    2 => HandleShowStatistics(gameResults),
                    0 => false,
                    _ => true
                };
            }

            Console.WriteLine("Роботу завершено.");
        }

        private static bool HandleNewGame(List<int> gameResults)
        {
            int attemptsUsed = PlayGame(MinNumber, MaxNumber, MaxAttempts);
            gameResults.Add(attemptsUsed);
            return true;
        }

        private static bool HandleShowStatistics(List<int> gameResults)
        {
            if (gameResults.Count == 0)
            {
                Console.WriteLine("\nНемає даних. Зіграйте хоча б одну гру.");
                return true;
            }

            int bestResult = FindMin(gameResults);
            double averageAttempts = CalculateAverage(gameResults);

            Console.WriteLine("\n=== Статистика за сеанс ===");
            Console.WriteLine($"Зіграно партій: {gameResults.Count}");
            Console.WriteLine($"Найкращий результат: {bestResult} спроб(и)");
            Console.WriteLine($"Середня кількість спроб: {averageAttempts:F2}");

            return true;
        }

        private static int PlayGame(int minNumber, int maxNumber, int maxAttempts)
        {
            Random random = new();
            int secretNumber = random.Next(minNumber, maxNumber + 1);

            Console.WriteLine($"\nЗагадано число від {minNumber} до {maxNumber}. У вас є {maxAttempts} спроб.");

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                int guess = ReadInt($"Спроба {attempt}/{maxAttempts}: ", minNumber, maxNumber);

                if (guess == secretNumber)
                {
                    Console.WriteLine($"Вітаємо! Ви вгадали число {secretNumber} за {attempt} спроб(и).");
                    return attempt;
                }

                int attemptsLeft = maxAttempts - attempt;
                string hint = guess < secretNumber ? "більше" : "менше";
                Console.WriteLine($"Загадане число {hint}. Спроб залишилось: {attemptsLeft}.");
            }

            Console.WriteLine($"На жаль, спроби вичерпано. Загадане число було: {secretNumber}.");
            return maxAttempts;
        }

        private static int FindMin(List<int> values)
        {
            int min = values[0];

            foreach (int value in values)
            {
                if (value < min)
                {
                    min = value;
                }
            }

            return min;
        }

        private static double CalculateAverage(List<int> values)
        {
            int sum = 0;

            foreach (int value in values)
            {
                sum += value;
            }

            return (double)sum / values.Count;
        }

        private static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Некоректне значення. Введіть ціле число від {min} до {max}.");
            }
        }
    }
}
