using ListStructureKit;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace ListStructureKitTests
{
    [TestFixture]
    public class StackLSKTests
    {
        [Test]
        public void Push_AddsElementToTopOfStack()
        {
            var stack = new StackLSK<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.That(stack.Size, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(3));
        }

        [Test]
        public void Pop_RemovesAndReturnsElementFromTopOfStack()
        {
            var stack = new StackLSK<int>(1, 2, 3);

            int poppedValue = stack.Pop();

            Assert.That(poppedValue, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(2));
            Assert.That(stack.Size, Is.EqualTo(2));
        }

        [Test]
        public void Pop_ThrowsException_WhenStackIsEmpty()
        {
            var stack = new StackLSK<int>();

            Assert.Throws<InvalidOperationException>(() => stack.Pop(), "Попытка получить элемент из пустого стека.");
        }

        [Test]
        public void Peek_ReturnsElementFromTopOfStackWithoutRemovingIt()
        {
            var stack = new StackLSK<char>('a', 'b', 'c');

            char peekedValue = stack.Peek();

            Assert.That(peekedValue, Is.EqualTo('c'));
            Assert.That(stack.Size, Is.EqualTo(3));
        }

        [Test]
        public void Peek_ThrowsException_WhenStackIsEmpty()
        {
            var stack = new StackLSK<int>();

            Assert.Throws<InvalidOperationException>(() => stack.Peek(), "Попытка получить элемент из пустого стека.");
        }

        [Test]
        public void Clear_EmptiesTheStack()
        {
            var stack = new StackLSK<double>(1.1, 2.2, 3.3);

            stack.Clear();

            Assert.That(stack.Size, Is.EqualTo(0));
            Assert.Throws<InvalidOperationException>(() => stack.Peek(), "Попытка получить элемент из пустого стека.");
        }

        [Test]
        public void IsEmpty_ReturnsTrue_WhenStackIsEmpty()
        {
            var stack = new StackLSK<int>();

            Assert.That(stack.IsEmpty(), Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_ReturnsFalse_WhenStackIsNotEmpty()
        {
            var stack = new StackLSK<int>();
            stack.Push(1);

            Assert.That(stack.IsEmpty(), Is.EqualTo(false));
        }

        [Test]
        public void Serialization_SerializesStackToJsonFile()
        {
            var stack = new StackLSK<string>("apple", "banana", "cherry");
            string filePath = "stack.json";

            stack.Serialization(filePath);

            Assert.That(File.Exists(filePath), Is.EqualTo(true));

            File.Delete(filePath);
        }

        [Test]
        public void Serialization_ThrowsException_WhenFileIsNotJson()
        {
            var stack = new StackLSK<int>(1, 2, 3);
            string filePath = "stack.txt";

            Assert.Throws<InvalidOperationException>(() => stack.Serialization(filePath), "Файл должен быть JSON.");
        }

        [Test]
        public void Deserialization_DeserializesStackFromJsonFile()
        {
            var stack = new StackLSK<string>("apple", "banana", "cherry");
            string filePath = "stack.json";
            stack.Serialization(filePath);

            var deserializedStack = StackLSK<string>.Deserialization(filePath);

            CollectionAssert.AreEqual(stack, deserializedStack!);

            File.Delete(filePath);
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileDoesNotExist()
        {
            string filePath = "nonexistent.json";

            Assert.Throws<InvalidOperationException>(() => StackLSK<int>.Deserialization(filePath), "Отсутствует файл по указанному пути.");
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileIsNotJson()
        {
            string filePath = "stack.txt";

            Assert.Throws<InvalidOperationException>(() => StackLSK<int>.Deserialization(filePath), "Файл должен быть JSON.");

            File.Delete(filePath);
        }
    }
}
