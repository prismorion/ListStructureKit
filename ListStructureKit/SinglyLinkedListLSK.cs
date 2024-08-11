using System.Collections;
using System.Runtime.Serialization.Json;

namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий однонаправленный список.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в списке.</typeparam>
    [Serializable]
    public class SinglyLinkedListLSK<T> : IEnumerable<T?>
    {
        /// <summary>
        /// Первый элемент списка.
        /// </summary>
        public SNode<T>? First { get; internal set; }        

        /// <summary>
        /// Последний элемент списка.
        /// </summary>
        public SNode<T>? Last { get; internal set; }

        /// <summary>
        /// Размер списка.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий список указанными элементами.
        /// </summary>
        /// <param name="values">Элементы для инициализации списка.</param>
        public SinglyLinkedListLSK(params T?[] values)
        {
            foreach (T? value in values)
                AddLast(value);
        }

        /// <summary>
        /// Конструктор, инициализирующий список элементами из переданной коллекции.
        /// </summary>
        /// <param name="collection">Коллекция элементов для инициализации списка.</param>
        public SinglyLinkedListLSK(IEnumerable<T> collection)
        {
            foreach (T value in collection)
                AddLast(value);
        }

        /// <summary>
        /// Добавляет элемент в начало списка.
        /// </summary>
        public void AddFirst(T? value)
        {
            SNode<T> node = new SNode<T>(value);
            if (First == null)
                Last = node;
            else
                node.Next = First;
            First = node;
            Size++;
        }

        /// <summary>
        /// Добавляет элемент в конец списка.
        /// </summary>
        public void AddLast(T? value)
        {
            SNode<T> node = new SNode<T>(value);
            if (Last == null)
                First = node;
            else
                Last.Next = node;
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
            SNode<T>? previous = null;
            SNode<T>? current = First;

            while (current != null)
            {
                if (existingValue!.Equals(current.Value))
                {
                    SNode<T> newNode = new SNode<T>(newValue);                    

                    if (previous != null)
                        previous.Next = newNode;
                    else
                        First = newNode;

                    newNode.Next = current;
                    Size++;
                    break;
                }

                previous = current;
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
            SNode<T>? current = First;

            while (current != null)
            {
                if (existingValue!.Equals(current.Value))
                {
                    SNode<T> newNode = new SNode<T>(newValue);                                       

                    if (current.Next != null)
                        newNode.Next = current.Next;
                    else
                        Last = newNode;

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
                First = First.Next;
            
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
                SNode<T>? current = First;
                while (current!.Next != Last)
                    current = current.Next;
                current.Next = null;
                Last = current;
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
            SNode<T>? previous = null;
            SNode<T>? current = First;

            while (current != null)
            {
                if (value!.Equals(current.Value))
                {
                    if (previous != null)
                        previous.Next = current.Next;
                    else
                        First = current.Next;

                    if (current.Next == null)
                        Last = previous;

                    Size--;
                    break;
                }

                previous = current;
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
        /// <param name="singlyLinkedList1">Первый список для объединения.</param>
        /// <param name="singlyLinkedList2">Второй список для объединения.</param>
        /// <returns>Новый список, содержащий копии элементов из обеих переданных списков.</returns>  
        public static SinglyLinkedListLSK<T>? Concat(SinglyLinkedListLSK<T> singlyLinkedList1, SinglyLinkedListLSK<T> singlyLinkedList2)
        {
            SinglyLinkedListLSK<T>? concatenatedList = new SinglyLinkedListLSK<T>();

            foreach (T? item in singlyLinkedList1)
                concatenatedList.AddLast(item);

            foreach (T? item in singlyLinkedList2)
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
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(GetType());
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    jsonFormatter.WriteObject(fs, this);
            }
            else
                throw new InvalidOperationException("Файл должен быть JSON.");
        }

        /// <summary>
        /// Десериализует список из файла JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет десериализован список.</param>
        /// <returns>Десериализованный список.</returns>
        public static SinglyLinkedListLSK<T>? Deserialization(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(SinglyLinkedListLSK<T>));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        return (SinglyLinkedListLSK<T>?)jsonFormatter.ReadObject(fs);
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
            SNode<T>? current = First;
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
