namespace ListStructureKit
{
    /// <summary>
    /// Класс, представляющий узел односвязного списка.
    /// </summary>
    /// <typeparam name="T">Тип данных, хранящихся в узле.</typeparam>
    [Serializable]
    public class SNode<T>
    {
        /// <summary>
        /// Значение узла.
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// Ссылка на следующий узел.
        /// </summary>
        public SNode<T>? Next { get; internal set; }

        /// <summary>
        /// Конструктор, инициализирующий узел с пустым значением.
        /// </summary>
        /// <param name="next">Ссылка на следующий узел.</param>
        public SNode(SNode<T>? next = null)
        {
            Next = next;
        }

        /// <summary>
        /// Конструктор, инициализирующий узел с заданным значением.
        /// </summary>
        /// <param name="value">Значение узла.</param>
        /// <param name="next">Ссылка на следующий узел.</param>
        public SNode(T? value, SNode<T>? next = null)
        {
            Value = value;
            Next = next;
        }

        /// <summary>
        /// Представление значения узла в виде строки.
        /// </summary>
        /// <returns>Строковое представление значения узла.</returns>
        public override string? ToString() => Value?.ToString();
    }
}
