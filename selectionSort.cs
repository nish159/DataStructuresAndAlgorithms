List<int> list = new List<int> { 5, 8, 3, 1, 2, 7, 6, 4 };

Print(list);
SelectionSort(list);
Print(list);

void SelectionSort(List<int> list)
{
    // time complexity nlog(n)
    for (int i = 0; i < list.Count; i++)
    {
        for (int j = i + 1; j < list.Count; j++)
        {
            if (list[j] < list[i])
            {
                int tmp = list[i];
                list[i] = list[j];
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
