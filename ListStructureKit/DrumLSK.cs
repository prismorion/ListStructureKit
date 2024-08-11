using System.Runtime.Serialization.Json;

namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий барабан.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в барабане.</typeparam>
    public class DrumLSK<T>
    {
        /// <summary>
        /// Вершина барабана.
        /// </summary>
        private DNode<T>? Top;

        /// <summary>
        /// Емкость барабана.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий барабан.
        /// </summary>
        /// <param name="capacity">Емкость барабана.</param>
        public DrumLSK(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Емкость должна быть больше нуля.");
            Capacity = capacity;
            InitializeDrum();
        }        

        /// <summary>
        /// Инициализация барабана с указанной емкостью.
        /// </summary>
        private void InitializeDrum()
        {
            Top = new DNode<T>();
            DNode<T> current = Top;
            for (int i = 1; i < Capacity; i++)
            {
                DNode<T> newNode = new DNode<T>();
                current.Next = newNode;
                newNode.Previous = current;
                current = newNode;
            }
            current.Next = Top;
            Top.Previous = current;
        }

        /// <summary>
        /// Вращает барабан по часовой стрелке.
        /// </summary>
        public void RotateClockwise()
        {
            Top = Top!.Next;
        }

        /// <summary>
        /// Вращает барабан против часовой стрелки.
        /// </summary>
        public void RotateCounterClockwise()
        {
            Top = Top!.Previous;
        }

        /// <summary>
        /// Записывает значение в текущий элемент (вершину) барабана.
        /// </summary>
        /// <param name="value">Записываемое значение.</param>
        public void Write(T value)
        {
            Top!.Value = value;            
        }

        /// <summary>
        /// Читает значение из текущего элемента (вершины) барабана.
        /// </summary>
        /// <returns>Значение текущего элемента.</returns>
        public T? Read()
        {
            return Top!.Value;
        }

        /// <summary>
        /// Очищает барабан, устанавливая значение каждого элемента в default.
        /// </summary>
        public void Clear()
        {
            DNode<T>? current = Top!;
            for (int i = 0; i < Capacity; i++)
            {
                current!.Value = default;
                current = current!.Next;
            }
        }

        /// <summary>
        /// Проверяет наличие указанного элемента в барабане.
        /// </summary>
        /// <param name="value">Искомое значение.</param>
        /// <returns>true, если значение найдено; в противном случае - false.</returns>
        public bool Contains(T value)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (Equals(Top!.Value, value))
                    return true;
                RotateClockwise();
            }
            return false;
        }

        /// <summary>
        /// Сериализует барабан в файл JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, по которому будет сериализован барабан.</param>
        public void Serialization(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(filePath);
                T?[] Items = new T[Capacity];
                for (int i = 0; i < Capacity; i++)
                {
                    Items[i] = Top!.Value;
                    RotateClockwise();
                }                    
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(Items.GetType());
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    jsonFormatter.WriteObject(fs, Items);
            }
            else
                throw new InvalidOperationException("Файл должен быть JSON.");
        }

        /// <summary>
        /// Десериализует барабан из файла JSON.
        /// </summary>
        /// <param name="filePath">Путь к файлу, из которого будет десериализован барабан.</param>
        /// <returns>Десериализованный барабан.</returns>
        public static DrumLSK<T>? Deserialization(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));
                    T[]? Items;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        Items = (T[]?)jsonFormatter.ReadObject(fs);
                    var drum = new DrumLSK<T>(Items!.Length);
                    for (int i = 0; i < drum.Capacity; i++)
                    {
                        drum!.Write(Items[i]);
                        drum.RotateClockwise();
                    }
                    return drum;
                }
                else
                    throw new InvalidOperationException("Файл должен быть JSON.");
            }
            else
                throw new InvalidOperationException("Отсутствует файл по указанному пути.");
        }
    }
}
