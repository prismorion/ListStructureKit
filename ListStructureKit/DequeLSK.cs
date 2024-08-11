using System.Collections;
using System.Runtime.Serialization.Json;

namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий дек.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в деке.</typeparam>
    public class DequeLSK<T> : IEnumerable<T?>
    {
        /// <summary>
        /// Первый элемент дека.
        /// </summary>
        private DNode<T>? First;

        /// <summary>
        /// Последний элемент дека.
        /// </summary>
        private DNode<T>? Last;

        /// <summary>
        /// Размер дека.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий дек указанными элементами.
        /// </summary>
        /// <param name="values">Элементы, с которыми инициализируется дек.</param>
        public DequeLSK(params T[] values)
        {
            foreach (T value in values)
                PushLast(value);
        }

        /// <summary>
        /// Конструктор, инициализирующий дек элементами из переданной коллекции.
        /// </summary>
        /// <param name="collection">Коллекция элементов для инициализации дека.</param>
        public DequeLSK(IEnumerable<T> collection)
        {
            foreach (T value in collection)
                PushLast(value);
        }

        /// <summary>
        /// Добавляет элемент в начало дека.
        /// </summary>
        public void PushFirst(T? value)
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
        /// Добавляет элемент в конец дека.
        /// </summary>
        public void PushLast(T? value)
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
        /// Удаляет и возвращает элемент из начала дека.
        /// </summary>
        /// <returns>Удаленное значение.</returns>
        public T? PopFirst()
        {
            if (First == null)
                throw new InvalidOperationException("Попытка удалить элемент из пустого дека.");

            T? value = First.Value;

            if (First == Last)
                First = Last = null;
            else
            {
                First = First.Next;
                if (First != null)
                    First.Previous = null;
            }
            Size--;
            return value;
        }

        /// <summary>
        /// Удаляет и возвращает элемент из конца дека.
        /// </summary>
        /// <returns>Удаленное значение.</returns>
        public T? PopLast()
        {
            if (Last == null)
                throw new InvalidOperationException("Попытка удалить элемент из пустого дека.");

            T? value = Last.Value;

            if (First == Last)
                First = Last = null;
            else
            {
                Last = Last.Previous;
                if (Last != null)
                    Last.Next = null;
            }
            Size--;
            return value;
        }

        /// <summary>
        /// Возвращает элемент из начала дека без его удаления.
        /// </summary>
        /// <returns>Значение первого элемента дека.</returns>
        public T? PeekFirst()
        {
            if (First == null)
                throw new InvalidOperationException("Попытка получить элемент из пустого дека.");

            return First.Value;
        }

        /// <summary>
        /// Возвращает элемент из конца стека без его удаления.
        /// </summary>
        /// <returns>Значение последнего элемента дека.</returns>
        public T? PeekLast()
        {
            if (Last == null)
                throw new InvalidOperationException("Попытка получить элемент из пустого дека.");

            return Last.Value;
        }

        /// <summary>
        /// Очищает дек.
        /// </summary>
        public void Clear()
        {
            First = null;
            Last = null;
            Size = 0;
        }

        /// <summary>
        /// Проверяет дек на пустоту.
        /// </summary>
        /// <returns>true, если дек пуст; в противном случае - false.</returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Сериализует дек в файл JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому будет сериализован дек.</param>
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
        /// Десериализует дек из файла JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет десериализован дек.</param>
        /// <returns>Десериализованный дек.</returns>
        public static DequeLSK<T>? Deserialization(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
                    List<T>? Items;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        Items = (List<T>?)jsonFormatter.ReadObject(fs);
                    var deque = new DequeLSK<T>();
                    foreach (var value in Items!)
                        deque.PushLast(value);
                    return deque;
                }
                else
                    throw new InvalidOperationException("Файл должен быть JSON.");
            }
            else
                throw new InvalidOperationException("Отсутствует файл по указанному пути.");
        }

        /// <summary>
        /// Перечислитель, позволяющий перебирать элементы в деке.
        /// </summary>
        /// <returns>Перечислитель для перебора элементов в деке.</returns>
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
