namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий узел двусвязного списка.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в узле.</typeparam>
    public class DNode<T>
    {
        /// <summary>
        /// Значение узла.
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// Ссылка на предыдущий узел.
        /// </summary>
        public DNode<T>? Previous { get; internal set; }

        /// <summary>
        /// Ссылка на следующий узел.
        /// </summary>
        public DNode<T>? Next { get; internal set; }

        /// <summary>
        /// Конструктор, инициализирующий узел с пустым значением.
        /// </summary>
        /// <param name="previous">Ссылка на предыдущий узел.</param>
        /// <param name="next">Ссылка на следующий узел.</param>
        public DNode(DNode<T>? previous = null, DNode<T>? next = null)
        {
            Previous = previous;
            Next = next;
        }

        /// <summary>
        /// Конструктор, инициализирующий узел с заданным значением.
        /// </summary>
        /// <param name="value">Значение узла.</param>
        /// <param name="previous">Ссылка на предыдущий узел.</param>
        /// <param name="next">Ссылка на следующий узел.</param>
        public DNode(T? value, DNode<T>? previous = null, DNode<T>? next = null)
        {
            Value = value;
            Previous = previous;
            Next = next;
        }

        /// <summary>
        /// Представление значения узла в виде строки.
        /// </summary>
        /// <returns>Строковое представление значения узла.</returns>
        public override string? ToString() => Value?.ToString();
    }
}
