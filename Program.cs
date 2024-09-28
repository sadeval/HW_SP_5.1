using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] numbers = Enumerable.Range(1, 1000000).ToArray(); 

        int taskCount = 4;

        int partSize = numbers.Length / taskCount;

        Task<long>[] tasks = new Task<long>[taskCount];

        for (int i = 0; i < taskCount; i++)
        {
            int start = i * partSize;
            int end = (i == taskCount - 1) ? numbers.Length : start + partSize;

            tasks[i] = Task.Run(() => CalculateSum(numbers, start, end));
        }

        Task.WhenAll(tasks).Wait();

        long totalSum = tasks.Sum(t => t.Result);

        Console.WriteLine($"Сумма всех элементов массива: {totalSum}");
    }

    static long CalculateSum(int[] numbers, int start, int end)
    {
        long sum = 0;
        for (int i = start; i < end; i++)
        {
            sum += numbers[i];
        }
        return sum;
    }
}
