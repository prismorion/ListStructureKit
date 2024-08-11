using System.Collections;
using System.Runtime.Serialization.Json;

namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий очередь.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в очереди.</typeparam>
    [Serializable]
    public class QueueLSK<T> : IEnumerable<T?>
    {
        /// <summary>
        /// Первый элемент очереди.
        /// </summary>
        public SNode<T>? First { get; internal set; }

        /// <summary>
        /// Последний элемент очереди.
        /// </summary>
        public SNode<T>? Last { get; internal set; }

        /// <summary>
        /// Размер очереди.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий очередь указанными элементами.
        /// </summary>
        /// <param name="values">Элементы для инициализации очереди.</param>
        public QueueLSK(params T?[] values)
        {
            foreach (T? value in values)
                Enqueue(value);
        }

        /// <summary>
        /// Конструктор, инициализирующий очередь элементами из переданной коллекции.
        /// </summary>
        /// <param name="collection">Коллекция элементов для инициализации очереди.</param>
        public QueueLSK(IEnumerable<T> collection)
        {
            foreach (T value in collection)
                Enqueue(value);
        }

        /// <summary>
        /// Добавляет элемент в конец очереди.
        /// </summary>
        /// <param name="value">Добавляемое значение.</param>
        public void Enqueue(T? value)
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
        /// Удаляет и возвращает элемент из начала очереди.
        /// </summary>
        /// <returns>Удаленное значение первого элемента очереди.</returns>
        public T? Dequeue()
        {
            if (First == null)
                throw new InvalidOperationException("Попытка получить элемент из пустой очереди.");

            T? value = First.Value;
            First = First.Next;
            Size--;

            if (First == null)
                Last = null;

            return value;
        }

        /// <summary>
        /// Возвращает элемент из начала очереди без его удаления.
        /// </summary>
        /// <returns>Значение первого элемента очереди.</returns>
        public T? Peek()
        {
            if (First == null)
                throw new InvalidOperationException("Попытка получить элемент из пустой очереди.");

            return First.Value;
        }

        /// <summary>
        /// Очищает очередь.
        /// </summary>
        public void Clear()
        {
            First = null;
            Last = null;
            Size = 0;
        }

        /// <summary>
        /// Объединяет две очереди в одну.
        /// </summary>
        /// <param name="queue1">Первая очередь для объединения.</param>
        /// <param name="queue2">Вторая очередь для объединения.</param>
        /// <returns>Новая очередь, содержащая копии элементов из обеих переданных очередей.</returns>       
        public static QueueLSK<T>? Concat(QueueLSK<T> queue1, QueueLSK<T> queue2)
        {
            QueueLSK<T>? concatenatedQueue = new QueueLSK<T>();

            foreach (T? item in queue1)
                concatenatedQueue.Enqueue(item);

            foreach (T? item in queue2)
                concatenatedQueue.Enqueue(item);

            return concatenatedQueue;
        }        

        /// <summary>
        /// Проверяет очередь на пустоту.
        /// </summary>
        /// <returns>true, если очередь пуста; в противном случае - false.</returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Сериализует очередь в файл JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому будет сериализована очередь.</param>
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
        /// Десериализует очередь из файла JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет десериализована очередь.</param>
        /// <returns>Десериализованная очередь.</returns>
        public static QueueLSK<T>? Deserialization(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(QueueLSK<T>));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        return (QueueLSK<T>?)jsonFormatter.ReadObject(fs);
                }
                else
                    throw new InvalidOperationException("Файл должен быть JSON.");
            }
            else
                throw new InvalidOperationException("Отсутствует файл по указанному пути.");
        }

        /// <summary>
        /// Перечислитель, позволяющий перебирать элементы в очереди.
        /// </summary>
        /// <returns>Перечислитель для перебора элементов в очереди.</returns>
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
