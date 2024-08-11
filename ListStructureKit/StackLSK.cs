using System.Collections;
using System.Runtime.Serialization.Json;

namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий стек.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в стеке.</typeparam>
    [Serializable]
    public class StackLSK<T> : IEnumerable<T?>
    {
        /// <summary>
        /// Верхний элемент стека.
        /// </summary>
        private SNode<T>? Top;

        /// <summary>
        /// Размер стека.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий стек указанными элементами.
        /// </summary>
        /// <param name="values">Элементы, с которыми инициализируется стек.</param>
        public StackLSK(params T[] values)
        {
            foreach (T value in values)
                Push(value);
        }

        /// <summary>
        /// Конструктор, инициализирующий стек элементами из переданной коллекции.
        /// </summary>
        /// <param name="collection">Коллекция элементов для инициализации стека.</param>
        public StackLSK(IEnumerable<T> collection)
        {
            foreach (T value in collection)
                Push(value);
        }

        /// <summary>
        /// Добавляет элемент в стек.
        /// </summary>
        /// <param name="value">Добавляемое значение.</param>
        public void Push(T? value)
        {
            SNode<T>? node = new SNode<T>(value, Top);
            Top = node;
            Size++;
        }

        /// <summary>
        /// Удаляет и возвращает элемент из вершины стека.
        /// </summary>
        /// <returns>Удаленное значение.</returns>
        public T? Pop()
        {
            if (Top == null)
                throw new InvalidOperationException("Попытка получить элемент из пустого стека.");

            T? value = Top.Value;
            Top = Top.Next;
            Size--;
            return value;
        }

        /// <summary>
        /// Возвращает элемент из вершины стека без его удаления.
        /// </summary>
        /// <returns>Значение верхнего элемента стека.</returns>
        public T? Peek()
        {
            if (Top == null)
                throw new InvalidOperationException("Попытка получить элемент из пустого стека.");

            return Top.Value;
        }

        /// <summary>
        /// Очищает стек.
        /// </summary>
        public void Clear()
        {
            Top = null;
            Size = 0;
        }

        /// <summary>
        /// Проверяет стек на пустоту.
        /// </summary>
        /// <returns>true, если стек пуст; в противном случае - false.</returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Сериализует стек в файл JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому будет сериализован стек.</param>
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
        /// Десериализует стек из файла JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет десериализован стек.</param>
        /// <returns>Десериализованный стек.</returns>
        public static StackLSK<T>? Deserialization(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(StackLSK<T>));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        return (StackLSK<T>?)jsonFormatter.ReadObject(fs);
                }
                else
                    throw new InvalidOperationException("Файл должен быть JSON.");
            }
            else
                throw new InvalidOperationException("Отсутствует файл по указанному пути.");
        }

        /// <summary>
        /// Перечислитель, позволяющий перебирать элементы в стеке.
        /// </summary>
        /// <returns>Перечислитель для перебора элементов в стеке.</returns>
        public IEnumerator<T?> GetEnumerator()
        {
            SNode<T>? current = Top;
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
