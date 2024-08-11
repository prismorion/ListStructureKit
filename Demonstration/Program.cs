using ListStructureKit;

internal class Program
{
    // Вывод коллекции элементов в консоль
    static void PrintCollection<T>(IEnumerable<T>? collection)
    {
        if (collection == null)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }

        foreach (var item in collection)
            Console.Write(item + " ");
        Console.WriteLine();
    }

    // Вывод элементов барабана в консоль
    static void PrintDrum<T>(DrumLSK<T> drum)
    {
        for (int i = 0; i < drum.Capacity; i++)
        {
            Console.Write(drum.Read() + " ");
            drum.RotateClockwise();
        }
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        //-------------------- QueueLSK --------------------
        Console.WriteLine("Класс: QueueLSK\n");
        
        // Создание очереди с использованием массива значений
        QueueLSK<int> queue1 = new QueueLSK<int>(1, 2, 3, 4, 5);
        Console.WriteLine("Инициализация очереди queue1 с использованием массива значений:");
        PrintCollection(queue1);
        Console.WriteLine();

        // Создание очереди с использованием коллекции
        List<int> initialListQueue = new List<int> { 6, 7, 8, 9, 10 };
        QueueLSK<int> queue2 = new QueueLSK<int>(initialListQueue);
        Console.WriteLine("Инициализация очереди queue2 с использованием коллекции:");
        PrintCollection(queue2);
        Console.WriteLine();

        // Объединение двух очередей
        QueueLSK<int>? concatenatedQueue = QueueLSK<int>.Concat(queue1, queue2);
        Console.WriteLine("Объединение двух очередей queue1 и queue2:");
        PrintCollection(concatenatedQueue);
        Console.WriteLine();

        // Добавление элементов в очередь
        queue1.Enqueue(11);
        queue1.Enqueue(12);
        Console.WriteLine("Добавление элементов 11 и 12 в очередь queue1:");
        PrintCollection(queue1);
        Console.WriteLine();

        // Извлечение элементов из очереди
        int? dequeuedValueQueue = queue1.Dequeue();
        Console.WriteLine($"Извлеченное значение из queue1: {dequeuedValueQueue}");
        Console.WriteLine("Очередь queue1 после извлечения элемента:");
        PrintCollection(queue1);
        Console.WriteLine();

        // Проверка первого элемента в очереди
        int? peekedValueQueue = queue1.Peek();
        Console.WriteLine($"Первый элемент в queue1 без его удаления: {peekedValueQueue}");
        Console.WriteLine("Очередь queue1 после получения элемента без удаления:");
        PrintCollection(queue1);
        Console.WriteLine();

        // Проверка на пустоту
        Console.WriteLine($"Очередь queue1 пуста: {queue1.IsEmpty()}");
        // Очистка очереди
        queue1.Clear();
        Console.WriteLine("Очистка очереди queue1.");
        // Проверка на пустоту после очистки
        Console.WriteLine($"Очередь queue1 пуста: {queue1.IsEmpty()}");
        Console.WriteLine();

        // Сериализация очереди
        string filePathQueue = "queue.json";
        concatenatedQueue.Serialization(filePathQueue);
        Console.WriteLine($"Очередь concatenatedQueue сериализована в файл: {filePathQueue}");

        // Десериализация очереди
        QueueLSK<int>? deserializedQueue = QueueLSK<int>.Deserialization(filePathQueue);
        Console.WriteLine("Очередь после десериализации:");
        PrintCollection(deserializedQueue);
        //--------------------------------------------------

        Console.WriteLine("\n\n\n");

        //-------------------- StackLSK --------------------
        Console.WriteLine("Класс: StackLSK\n");

        // Создание стека с использованием массива значений
        StackLSK<int> stack1 = new StackLSK<int>(1, 2, 3, 4, 5);
        Console.WriteLine("Инициализация стека stack1 с использованием массива значений:");
        PrintCollection(stack1);
        Console.WriteLine();

        // Создание стека с использованием коллекции
        List<int> initialListStack = new List<int> { 6, 7, 8, 9, 10 };
        StackLSK<int> stack2 = new StackLSK<int>(initialListStack);
        Console.WriteLine("Инициализация стека stack2 с использованием коллекции:");
        PrintCollection(stack2);
        Console.WriteLine();

        // Добавление элементов в стек
        stack1.Push(11);
        stack1.Push(12);
        Console.WriteLine("Добавление элементов 11 и 12 в стек stack1:");
        PrintCollection(stack1);
        Console.WriteLine();

        // Извлечение элементов из стека
        int? poppedValueStack = stack1.Pop();
        Console.WriteLine($"Извлеченное значение из stack1: {poppedValueStack}");
        Console.WriteLine("Стек stack1 после извлечения элемента:");
        PrintCollection(stack1);
        Console.WriteLine();

        // Проверка верхнего элемента стека без его удаления
        int? peekedValueStack = stack1.Peek();
        Console.WriteLine($"Верхний элемент в stack1 без его удаления: {peekedValueStack}");
        Console.WriteLine("Стек stack1 после получения верхнего элемента без удаления:");
        PrintCollection(stack1);
        Console.WriteLine();

        // Проверка на пустоту
        Console.WriteLine($"Стек stack1 пуст: {stack1.IsEmpty()}");
        // Очистка стека
        stack1.Clear();
        Console.WriteLine("Очистка стека stack1.");
        // Проверка на пустоту после очистки
        Console.WriteLine($"Стек stack1 пуст: {stack1.IsEmpty()}");
        Console.WriteLine();

        // Сериализация стека
        string filePathStack = "stack.json";
        stack2.Serialization(filePathStack);
        Console.WriteLine($"Стек stack2 сериализован в файл: {filePathStack}");

        // Десериализация стека
        StackLSK<int>? deserializedStack = StackLSK<int>.Deserialization(filePathStack);
        Console.WriteLine("Стек после десериализации:");
        PrintCollection(deserializedStack);
        //--------------------------------------------------

        Console.WriteLine("\n\n\n");

        //-------------------- DequeLSK --------------------
        Console.WriteLine("Класс: DequeLSK\n");

        // Создание дека с использованием массива значений
        DequeLSK<int> deque1 = new DequeLSK<int>(1, 2, 3, 4, 5);
        Console.WriteLine("Инициализация дека deque1 с использованием массива значений:");
        PrintCollection(deque1);
        Console.WriteLine();

        // Создание дека с использованием коллекции
        List<int> initialList = new List<int> { 6, 7, 8, 9, 10 };
        DequeLSK<int> deque2 = new DequeLSK<int>(initialList);
        Console.WriteLine("Инициализация дека deque2 с использованием коллекции:");
        PrintCollection(deque2);
        Console.WriteLine();

        // Добавление элементов в начало и конец дека
        deque1.PushFirst(0);
        deque1.PushLast(6);
        Console.WriteLine("Добавление элементов 0 в начало и 6 в конец дека deque1:");
        PrintCollection(deque1);
        Console.WriteLine();

        // Удаление элементов из начала и конца дека
        int? poppedFirst = deque1.PopFirst();
        int? poppedLast = deque1.PopLast();
        Console.WriteLine($"Извлеченное значение из начала deque1: {poppedFirst}");
        Console.WriteLine($"Извлеченное значение из конца deque1: {poppedLast}");
        Console.WriteLine("Дек deque1 после извлечения элементов:");
        PrintCollection(deque1);
        Console.WriteLine();

        // Проверка первого и последнего элемента дека без их удаления
        int? peekedFirst = deque1.PeekFirst();
        int? peekedLast = deque1.PeekLast();
        Console.WriteLine($"Первый элемент в deque1 без его удаления: {peekedFirst}");
        Console.WriteLine($"Последний элемент в deque1 без его удаления: {peekedLast}");
        Console.WriteLine("Дек deque1 после получения элементов без удаления:");
        PrintCollection(deque1);
        Console.WriteLine();

        // Проверка на пустоту
        Console.WriteLine($"Дек deque1 пуст: {deque1.IsEmpty()}");
        // Очистка дека
        deque1.Clear();
        Console.WriteLine("Очистка дека deque1.");
        // Проверка на пустоту после очистки
        Console.WriteLine($"Дек deque1 пуст: {deque1.IsEmpty()}");
        Console.WriteLine();

        // Сериализация дека
        string filePath = "deque.json";
        deque2.Serialization(filePath);
        Console.WriteLine($"Дек deque2 сериализован в файл: {filePath}");

        // Десериализация дека
        DequeLSK<int>? deserializedDeque = DequeLSK<int>.Deserialization(filePath);
        Console.WriteLine("Дек после десериализации:");
        PrintCollection(deserializedDeque);
        //--------------------------------------------------

        Console.WriteLine("\n\n\n");

        //-------------------- DrumLSK --------------------
        Console.WriteLine("Класс: DrumLSK\n");

        // Создание барабана с емкостью 5
        DrumLSK<int> drum = new DrumLSK<int>(5);
        Console.WriteLine("Инициализация барабана с емкостью 5.");
        Console.WriteLine();

        // Запись значений в барабан
        for (int i = 0; i < 5; i++)
        {
            drum.Write(i);
            drum.RotateClockwise();
        }
        Console.WriteLine("В барабан записаны значения: 0, 1, 2, 3, 4");
        Console.WriteLine();

        // Чтение значений из барабана
        Console.WriteLine("Чтение значений из барабана:");
        for (int i = 0; i < 5; i++)
        {
            Console.Write(drum.Read() + " ");
            drum.RotateClockwise();
        }
        Console.WriteLine("\n");

        // Вращение барабана
        drum.RotateClockwise();
        Console.WriteLine("Барабан после вращения по часовой стрелке:");
        PrintDrum(drum);
        Console.WriteLine();

        drum.RotateCounterClockwise();
        drum.RotateCounterClockwise();
        Console.WriteLine("Барабан после двойного вращения против часовой стрелки:");
        PrintDrum(drum);
        Console.WriteLine();

        // Проверка наличия элемента
        int valueToCheck = 1;
        bool containsValue = drum.Contains(valueToCheck);
        Console.WriteLine($"Барабан содержит значение {valueToCheck}: {containsValue}");
        Console.WriteLine("Барабан после проверки наличия элемента:");
        PrintDrum(drum);
        Console.WriteLine();

        // Сериализация барабана
        string filePathDrum = "drum.json";
        drum.Serialization(filePathDrum);
        Console.WriteLine($"Барабан сериализован в файл: {filePathDrum}");

        // Десериализация барабана
        DrumLSK<int>? deserializedDrum = DrumLSK<int>.Deserialization(filePathDrum);
        Console.WriteLine("Барабан после десериализации:");
        PrintDrum(deserializedDrum!);
        Console.WriteLine();

        // Очистка барабана
        drum.Clear();
        Console.WriteLine("Барабан после очистки:");
        PrintDrum(drum);
        //--------------------------------------------------

        Console.WriteLine("\n\n\n\n");

        //-------------- SinglyLinkedListLSK ---------------
        Console.WriteLine("Класс: SinglyLinkedListLSK\n");

        // Создание списка с использованием массива значений
        SinglyLinkedListLSK<int> singlyList1 = new SinglyLinkedListLSK<int>(1, 2, 3, 4, 5);
        Console.WriteLine("Инициализация списка singlyList1 с использованием массива значений:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Создание списка с использованием коллекции
        List<int> initialListSinglyList = new List<int> { 6, 7, 8, 9, 10 };
        SinglyLinkedListLSK<int> singlyList2 = new SinglyLinkedListLSK<int>(initialListSinglyList);
        Console.WriteLine("Инициализация списка singlyList2 с использованием коллекции:");
        PrintCollection(singlyList2);
        Console.WriteLine();

        // Объединение двух списков
        SinglyLinkedListLSK<int>? concatenatedSinglyList = SinglyLinkedListLSK<int>.Concat(singlyList1, singlyList2);
        Console.WriteLine("Объединение двух списков singlyList1 и singlyList2:");
        PrintCollection(concatenatedSinglyList);
        Console.WriteLine();

        // Добавление элемента в начало списка
        singlyList1.AddFirst(0);
        Console.WriteLine("Добавление элемента 0 в начало списка singlyList1:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Добавление элемента в конец списка
        singlyList1.AddLast(6);
        Console.WriteLine("Добавление элемента 6 в конец списка singlyList1:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Добавление элемента перед указанным элементом
        singlyList1.AddBefore(3, 2);
        Console.WriteLine("Добавление элемента 2 перед элементом 3 в списке singlyList1:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Добавление элемента после указанного элемента
        singlyList1.AddAfter(3, 4);
        Console.WriteLine("Добавление элемента 4 после элемента 3 в списке singlyList1:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Удаление первого элемента
        int? removedFirstSinglyList = singlyList1.RemoveFirst();
        Console.WriteLine($"Удаленное значение первого элемента из singlyList1: {removedFirstSinglyList}");
        Console.WriteLine("Список singlyList1 после удаления первого элемента:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Удаление последнего элемента
        int? removedLastSinglyList = singlyList1.RemoveLast();
        Console.WriteLine($"Удаленное значение последнего элемента из singlyList1: {removedLastSinglyList}");
        Console.WriteLine("Список singlyList1 после удаления последнего элемента:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Удаление первого вхождения элемента
        singlyList1.Remove(2);
        Console.WriteLine($"Удаление первого вхождения элемента 2 из singlyList1.");
        Console.WriteLine("Список singlyList1 после удаления первого вхождения элемента 2:");
        PrintCollection(singlyList1);
        Console.WriteLine();

        // Проверка на пустоту
        Console.WriteLine($"Список singlyList1 пуст: {singlyList1.IsEmpty()}");
        // Очистка списка
        singlyList1.Clear();
        Console.WriteLine("Очистка списка singlyList1.");
        // Проверка на пустоту после очистки
        Console.WriteLine($"Список singlyList1 пуст: {singlyList1.IsEmpty()}");
        Console.WriteLine();

        // Сериализация списка
        string filePathSinglyList = "singlyList.json";
        concatenatedSinglyList!.Serialization(filePathSinglyList);
        Console.WriteLine($"Список concatenatedSinglyList сериализован в файл: {filePathSinglyList}");

        // Десериализация списка
        SinglyLinkedListLSK<int>? deserializedSinglyList = SinglyLinkedListLSK<int>.Deserialization(filePathSinglyList);
        Console.WriteLine("Список после десериализации:");
        PrintCollection(deserializedSinglyList);
        //--------------------------------------------------

        Console.WriteLine("\n\n\n");

        //-------------- DoublyLinkedListLSK ---------------
        Console.WriteLine("Класс: DoublyLinkedListLSK\n");

        // Создание списка с использованием массива значений
        DoublyLinkedListLSK<int> doublyList1 = new DoublyLinkedListLSK<int>(1, 2, 3, 4, 5);
        Console.WriteLine("Инициализация списка doublyList1 с использованием массива значений:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Создание списка с использованием коллекции
        List<int> initialListDoublyList = new List<int> { 6, 7, 8, 9, 10 };
        DoublyLinkedListLSK<int> doublyList2 = new DoublyLinkedListLSK<int>(initialListDoublyList);
        Console.WriteLine("Инициализация списка doublyList2 с использованием коллекции:");
        PrintCollection(doublyList2);
        Console.WriteLine();

        // Объединение двух списков
        DoublyLinkedListLSK<int>? concatenatedDoublyList = DoublyLinkedListLSK<int>.Concat(doublyList1, doublyList2);
        Console.WriteLine("Объединение двух списков doublyList1 и doublyList2:");
        PrintCollection(concatenatedDoublyList);
        Console.WriteLine();

        // Добавление элемента в начало списка
        doublyList1.AddFirst(0);
        Console.WriteLine("Добавление элемента 0 в начало списка doublyList1:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Добавление элемента в конец списка
        doublyList1.AddLast(6);
        Console.WriteLine("Добавление элемента 6 в конец списка doublyList1:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Добавление элемента перед указанным элементом
        doublyList1.AddBefore(3, 2);
        Console.WriteLine("Добавление элемента 2 перед элементом 3 в списке doublyList1:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Добавление элемента после указанного элемента
        doublyList1.AddAfter(3, 4);
        Console.WriteLine("Добавление элемента 4 после элемента 3 в списке doublyList1:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Удаление первого элемента
        int? removedFirstDoublyList = doublyList1.RemoveFirst();
        Console.WriteLine($"Удаленное значение первого элемента из doublyList1: {removedFirstDoublyList}");
        Console.WriteLine("Список doublyList1 после удаления первого элемента:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Удаление последнего элемента
        int? removedLastDoublyList = doublyList1.RemoveLast();
        Console.WriteLine($"Удаленное значение последнего элемента из doublyList1: {removedLastDoublyList}");
        Console.WriteLine("Список doublyList1 после удаления последнего элемента:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Удаление первого вхождения элемента
        doublyList1.Remove(2);
        Console.WriteLine($"Удаление первого вхождения элемента 2 из doublyList1.");
        Console.WriteLine("Список doublyList1 после удаления первого вхождения элемента 2:");
        PrintCollection(doublyList1);
        Console.WriteLine();

        // Проверка на пустоту
        Console.WriteLine($"Список doublyList1 пуст: {doublyList1.IsEmpty()}");
        // Очистка списка
        doublyList1.Clear();
        Console.WriteLine("Очистка списка doublyList1.");
        // Проверка на пустоту после очистки
        Console.WriteLine($"Список doublyList1 пуст: {doublyList1.IsEmpty()}");
        Console.WriteLine();

        // Сериализация списка
        string filePathDoublyList = "doublyList.json";
        concatenatedDoublyList!.Serialization(filePathDoublyList);
        Console.WriteLine($"Список concatenatedDoublyList сериализован в файл: {filePathDoublyList}");

        // Десериализация списка
        DoublyLinkedListLSK<int>? deserializedDoublyList = DoublyLinkedListLSK<int>.Deserialization(filePathDoublyList);
        Console.WriteLine("Список после десериализации:");
        PrintCollection(deserializedDoublyList);
        //--------------------------------------------------

        Console.WriteLine("\n\n\n");
    }
}
