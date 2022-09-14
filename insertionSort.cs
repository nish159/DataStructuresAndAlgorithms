List<int> list = new List<int> { 5, 8, 3, 1, 2, 7, 6, 4 };

Print(list);
InsertionSort(list);
Print(list);

void InsertionSort(List<int> list)
{
    for (int i = 0; i < list.Count - 1; i++)
    {
        if (list[i + 1] < list[i])
        {
            int index = i + 1;

            while (index >= 1 && list[index] < list[index - 1])
            {
                int tmp = list[index];
                list[index] = list[index - 1];
                list[index - 1] = tmp;

                index--;
            }
        }
    }
}

void Print(List<int> list)
{
    foreach (var num in list)
    {
        Console.Write(num + " ");
    }
    Console.WriteLine();
}
