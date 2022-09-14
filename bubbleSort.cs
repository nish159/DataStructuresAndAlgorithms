List<int> list = new List<int> { 5, 8, 3, 1, 2, 7, 6, 4 };

Print(list);
BubbleSort(list);
Print(list);

void BubbleSort(List<int> list)
{
    for (int i = 0; i < list.Count; i++)
    {
        for (int j = 0; j < list.Count - 1; j++)
        {
            if (list[j + 1] < list[j])
            {
                int tmp = list[j + 1];
                list[j + 1] = list[j];
                list[j] = tmp;
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
