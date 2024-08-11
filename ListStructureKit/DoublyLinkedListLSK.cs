using System.Collections;
using System.Runtime.Serialization.Json;

namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий двунаправленный список.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в списке.</typeparam>
    public class DoublyLinkedListLSK<T> : IEnumerable<T?>
    {
        /// <summary>
        /// Первый элемент списка.
        /// </summary>
        public DNode<T>? First { get; internal set; }

        /// <summary>
        /// Последний элемент списка.
        /// </summary>
        public DNode<T>? Last { get; internal set; }

        /// <summary>
        /// Размер списка.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий список указанными элементами.
        /// </summary>
        /// <param name="values">Элементы для инициализации списка.</param>
        public DoublyLinkedListLSK(params T?[] values)
        {
            foreach (T? value in values)
                AddLast(value);
        }

        /// <summary>
        /// Конструктор, инициализирующий список элементами из переданной коллекции.
        /// </summary>
        /// <param name="collection">Коллекция элементов для инициализации списка.</param>
        public DoublyLinkedListLSK(IEnumerable<T> collection)
        {
            foreach (T value in collection)
                AddLast(value);
        }

        /// <summary>
        /// Добавляет элемент в начало списка.
        /// </summary>
        public void AddFirst(T? value)
        {
            DNode<T> node = new DNode<T>(value);
            if (First == null)
                Last = node;
            else
            {
                node.Next = First;
                First.Previous = node;                
            }                
            First = node;
            Size++;
        }

        /// <summary>
        /// Добавляет элемент в конец списка.
        /// </summary>
        public void AddLast(T? value)
        {
            DNode<T> node = new DNode<T>(value);
            if (Last == null)
                First = node;
            else
            {
                node.Previous = Last;
                Last.Next = node;
            }                
            Last = node;
            Size++;
        }

        /// <summary>
        /// Добавляет новый элемент перед указанным элементом в списке.
        /// </summary>
        /// <param name="existingValue">Значение элемента, перед которым добавится новый элемент.</param>
        /// <param name="newValue">Добавляемое значение элемента.</param>
        public void AddBefore(T existingValue, T? newValue)
        {            
            DNode<T>? current = First;

            while (current != null)
            {
                if (existingValue!.Equals(current.Value))
                {
                    DNode<T> newNode = new DNode<T>(newValue);

                    if (current.Previous != null)
                    {
                        current.Previous.Next = newNode;
                        newNode.Previous = current.Previous;
                    }
                    else
                        First = newNode;

                    newNode.Next = current;
                    current.Previous = newNode;
                    Size++;
                    break;
                }
                current = current.Next;
            }
        }

        /// <summary>
        /// Добавляет новый элемент после указанного элемента в списке.
        /// </summary>
        /// <param name="existingValue">Значение элемента, после которого добавится новый элемент.</param>
        /// <param name="newValue">Добавляемое значение элемента.</param>
        public void AddAfter(T existingValue, T? newValue)
        {            
            DNode<T>? current = First;

            while (current != null)
            {
                if (existingValue!.Equals(current.Value))
                {
                    DNode<T> newNode = new DNode<T>(newValue);

                    if (current.Next != null)
                    {
                        current.Next.Previous = newNode;
                        newNode.Next = current.Next;
                    }
                    else
                        Last = newNode;
                    
                    newNode.Previous = current;
                    current.Next = newNode;
                    Size++;
                    break;
                }
                current = current.Next;
            }
        }

        /// <summary>
        /// Удаляет и возвращает элемент из начала списка.
        /// </summary>
        /// <returns>Удаленное значение первого элемента списка.</returns>
        public T? RemoveFirst()
        {
            if (First == null)
                throw new InvalidOperationException("Попытка удалить элемент из пустого списка.");

            T? value = First.Value;

            if (First == Last)
                First = Last = null;
            else
            {
                First = First.Next;
                First!.Previous = null;
            }
            Size--;

            return value;
        }

        /// <summary>
        /// Удаляет и возвращает элемент из конца списка.
        /// </summary>
        /// <returns>Удаленное значение последнего элемента списка.</returns>
        public T? RemoveLast()
        {
            if (Last == null)
                throw new InvalidOperationException("Попытка удалить элемент из пустого списка.");

            T? value = Last.Value;

            if (First == Last)
                First = Last = null;
            else
            {
                Last = Last.Previous;
                Last!.Next = null;
            }
            Size--;

            return value;
        }

        /// <summary>
        /// Удаляет первое вхождение элемента списка.
        /// </summary>
        /// <param name="value">Значение элемента.</param>
        public void Remove(T? value)
        {
            DNode<T>? current = First;

            while (current != null)
            {
                if (value!.Equals(current.Value))
                {
                    if (current.Previous != null)
                        current.Previous.Next = current.Next;
                    else
                        First = current.Next;

                    if (current.Next == null)
                        Last = current.Previous;                    
                    else
                        current.Next.Previous = current.Previous;

                    Size--;
                    break;
                }

                current = current.Next;
            }
        }

        /// <summary>
        /// Очищает список.
        /// </summary>
        public void Clear()
        {
            First = null;
            Last = null;
            Size = 0;
        }

        /// <summary>
        /// Объединяет два списка в один.
        /// </summary>
        /// <param name="doublyLinkedList1">Первый список для объединения.</param>
        /// <param name="doublyLinkedList2">Второй список для объединения.</param>
        /// <returns>Новый список, содержащий копии элементов из обеих переданных списков.</returns>       
        public static DoublyLinkedListLSK<T>? Concat(DoublyLinkedListLSK<T> doublyLinkedList1, DoublyLinkedListLSK<T> doublyLinkedList2)
        {
            DoublyLinkedListLSK<T>? concatenatedList = new DoublyLinkedListLSK<T>();

            foreach (T? item in doublyLinkedList1)
                concatenatedList.AddLast(item);

            foreach (T? item in doublyLinkedList2)
                concatenatedList.AddLast(item);

            return concatenatedList;
        }

        /// <summary>
        /// Проверяет список на пустоту.
        /// </summary>
        /// <returns>true, если список пуст; в противном случае - false.</returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Сериализует список в файл JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому будет сериализован список.</param>
        public void Serialization(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(filePath);
                List<T?> Items = new List<T?>();
                foreach (T? value in this)
                    Items.Add(value);
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(Items.GetType());
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    jsonFormatter.WriteObject(fs, Items);
            }
            else
                throw new InvalidOperationException("Файл должен быть JSON.");
        }

        /// <summary>
        /// Десериализует список из файла JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет десериализован список.</param>
        /// <returns>Десериализованный список.</returns>
        public static DoublyLinkedListLSK<T>? Deserialization(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
                    List<T>? Items;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        Items = (List<T>?)jsonFormatter.ReadObject(fs);
                    var deque = new DoublyLinkedListLSK<T>();
                    foreach (var value in Items!)
                        deque.AddLast(value);
                    return deque;
                }
                else
                    throw new InvalidOperationException("Файл должен быть JSON.");
            }
            else
                throw new InvalidOperationException("Отсутствует файл по указанному пути.");
        }

        /// <summary>
        /// Перечислитель, позволяющий перебирать элементы в списке.
        /// </summary>
        /// <returns>Перечислитель для перебора элементов в списке.</returns>
        public IEnumerator<T?> GetEnumerator()
        {
            DNode<T>? current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        // Поддержка оператора foreach
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
