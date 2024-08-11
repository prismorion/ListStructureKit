using NUnit.Framework;
using ListStructureKit;
using NUnit.Framework.Legacy;

namespace ListStructureKitTests
{
    [TestFixture]
    public class DequeLSKTests
    {
        [Test]
        public void PushFirst_AddsElementToFrontOfDeque()
        {
            var deque = new DequeLSK<int>();
            deque.PushFirst(1);
            deque.PushFirst(2);
            deque.PushFirst(3);

            Assert.That(deque.Size, Is.EqualTo(3));
            Assert.That(deque.PeekFirst(), Is.EqualTo(3));
            Assert.That(deque.PeekLast(), Is.EqualTo(1));
        }

        [Test]
        public void PushLast_AddsElementToBackOfDeque()
        {
            var deque = new DequeLSK<int>();
            deque.PushLast(1);
            deque.PushLast(2);
            deque.PushLast(3);

            Assert.That(deque.Size, Is.EqualTo(3));
            Assert.That(deque.PeekLast(), Is.EqualTo(3));
            Assert.That(deque.PeekFirst(), Is.EqualTo(1));
        }

        [Test]
        public void PopFirst_RemovesAndReturnsElementFromFrontOfDeque()
        {
            var deque = new DequeLSK<int>(1, 2, 3);

            int poppedValue = deque.PopFirst();

            Assert.That(poppedValue, Is.EqualTo(1));
            Assert.That(deque.PeekFirst(), Is.EqualTo(2));
            Assert.That(deque.Size, Is.EqualTo(2));
        }

        [Test]
        public void PopFirst_ThrowsException_WhenDequeIsEmpty()
        {
            var deque = new DequeLSK<int>();

            Assert.Throws<InvalidOperationException>(() => deque.PopFirst(), "Попытка удалить элемент из пустого дека.");
        }

        [Test]
        public void PopLast_RemovesAndReturnsElementFromBackOfDeque()
        {
            var deque = new DequeLSK<int>(1, 2, 3);

            int poppedValue = deque.PopLast();

            Assert.That(poppedValue, Is.EqualTo(3));
            Assert.That(deque.PeekLast(), Is.EqualTo(2));
            Assert.That(deque.Size, Is.EqualTo(2));
        }

        [Test]
        public void PopLast_ThrowsException_WhenDequeIsEmpty()
        {
            var deque = new DequeLSK<int>();

            Assert.Throws<InvalidOperationException>(() => deque.PopLast(), "Попытка удалить элемент из пустого дека.");
        }

        [Test]
        public void PeekFirst_ReturnsElementFromFrontOfDequeWithoutRemovingIt()
        {
            var deque = new DequeLSK<char>('a', 'b', 'c');

            char peekedValue = deque.PeekFirst();

            Assert.That(peekedValue, Is.EqualTo('a'));
            Assert.That(deque.Size, Is.EqualTo(3));
        }

        [Test]
        public void PeekFirst_ThrowsException_WhenDequeIsEmpty()
        {
            var deque = new DequeLSK<int>();

            Assert.Throws<InvalidOperationException>(() => deque.PeekFirst(), "Попытка получить элемент из пустого дека.");
        }

        [Test]
        public void PeekLast_ReturnsElementFromBackOfDequeWithoutRemovingIt()
        {
            var deque = new DequeLSK<char>('a', 'b', 'c');

            char peekedValue = deque.PeekLast();

            Assert.That(peekedValue, Is.EqualTo('c'));
            Assert.That(deque.Size, Is.EqualTo(3));
        }

        [Test]
        public void PeekLast_ThrowsException_WhenDequeIsEmpty()
        {
            var deque = new DequeLSK<int>();

            Assert.Throws<InvalidOperationException>(() => deque.PeekLast(), "Попытка получить элемент из пустого дека.");
        }

        [Test]
        public void Clear_EmptiesTheDeque()
        {
            var deque = new DequeLSK<double>(1.1, 2.2, 3.3);

            deque.Clear();

            Assert.That(deque.Size, Is.EqualTo(0));
            Assert.Throws<InvalidOperationException>(() => deque.PeekFirst(), "Попытка получить элемент из пустого дека.");
            Assert.Throws<InvalidOperationException>(() => deque.PeekLast(), "Попытка получить элемент из пустого дека.");
        }

        [Test]
        public void IsEmpty_ReturnsTrue_WhenDequeIsEmpty()
        {
            var deque = new DequeLSK<int>();

            Assert.That(deque.IsEmpty(), Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_ReturnsFalse_WhenDequeIsNotEmpty()
        {
            var deque = new DequeLSK<int>();
            deque.PushFirst(1);

            Assert.That(deque.IsEmpty(), Is.EqualTo(false));
        }

        [Test]
        public void Serialization_SerializesDequeToJsonFile()
        {
            var deque = new DequeLSK<string>("apple", "banana", "cherry");
            string filePath = "deque.json";

            deque.Serialization(filePath);

            Assert.That(File.Exists(filePath), Is.EqualTo(true));

            File.Delete(filePath);
        }

        [Test]
        public void Serialization_ThrowsException_WhenFileIsNotJson()
        {
            var deque = new DequeLSK<int>(1, 2, 3);
            string filePath = "deque.txt";

            Assert.Throws<InvalidOperationException>(() => deque.Serialization(filePath), "Файл должен быть JSON.");
        }

        [Test]
        public void Deserialization_DeserializesDequeFromJsonFile()
        {
            var deque = new DequeLSK<string>("apple", "banana", "cherry");
            string filePath = "deque.json";
            deque.Serialization(filePath);

            var deserializedDeque = DequeLSK<string>.Deserialization(filePath);

            CollectionAssert.AreEqual(deque, deserializedDeque!);

            File.Delete(filePath);
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileDoesNotExist()
        {
            string filePath = "nonexistent.json";

            Assert.Throws<InvalidOperationException>(() => DequeLSK<int>.Deserialization(filePath), "Отсутствует файл по указанному пути.");
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileIsNotJson()
        {
            string filePath = "deque.txt";

            Assert.Throws<InvalidOperationException>(() => DequeLSK<int>.Deserialization(filePath), "Файл должен быть JSON.");

            File.Delete(filePath);
        }
    }
}
