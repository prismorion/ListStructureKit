using NUnit.Framework;
using ListStructureKit;
using NUnit.Framework.Legacy;

namespace ListStructureKitTests
{
    [TestFixture]
    public class QueueLSKTests
    {
        [Test]
        public void Enqueue_AddsElementToEndOfQueue()
        {
            var queue = new QueueLSK<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Assert.That(queue.Size, Is.EqualTo(3));
            Assert.That(queue.First!.Value, Is.EqualTo(1));
            Assert.That(queue.Last!.Value, Is.EqualTo(3));
        }

        [Test]
        public void Dequeue_RemovesAndReturnsElementFromStartOfQueue()
        {
            var queue = new QueueLSK<int>(1, 2, 3);

            int dequeuedValue = queue.Dequeue();

            Assert.That(dequeuedValue, Is.EqualTo(1));
            Assert.That(queue.First!.Value, Is.EqualTo(2));
            Assert.That(queue.Size, Is.EqualTo(2));
        }

        [Test]
        public void Dequeue_ThrowsException_WhenQueueIsEmpty()
        {
            var queue = new QueueLSK<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue(), "Попытка получить элемент из пустой очереди.");
        }

        [Test]
        public void Peek_ReturnsElementFromStartOfQueueWithoutRemovingIt()
        {
            var queue = new QueueLSK<char>('a', 'b', 'c');

            char? peekedValue = queue.Peek();

            Assert.That(peekedValue, Is.EqualTo('a'));
            Assert.That(queue.Size, Is.EqualTo(3));
        }

        [Test]
        public void Peek_ThrowsException_WhenQueueIsEmpty()
        {
            var queue = new QueueLSK<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Peek(), "Попытка получить элемент из пустой очереди.");
        }

        [Test]
        public void Clear_EmptiesTheQueue()
        {
            var queue = new QueueLSK<double>(1.1, 2.2, 3.3);

            queue.Clear();

            Assert.That(queue.Size, Is.EqualTo(0));
            Assert.That(queue.First, Is.EqualTo(null));
            Assert.That(queue.Last, Is.EqualTo(null));
        }

        [Test]
        public void Concat_MergesTwoQueues()
        {
            var queue1 = new QueueLSK<int>(1, 2, 3);
            var queue2 = new QueueLSK<int>(4, 5, 6);

            var concatenatedQueue = QueueLSK<int>.Concat(queue1, queue2);

            Assert.That(concatenatedQueue!.Size, Is.EqualTo(6));

            int[] expectedValues = { 1, 2, 3, 4, 5, 6 };
            int index = 0;
            foreach (var item in concatenatedQueue)
            {
                Assert.That(item, Is.EqualTo(expectedValues[index++]));
            }
        }

        [Test]
        public void IsEmpty_ReturnsTrue_WhenQueueIsEmpty()
        {
            var queue = new QueueLSK<int>();

            Assert.That(queue.IsEmpty(), Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_ReturnsFalse_WhenQueueIsNotEmpty()
        {
            var queue = new QueueLSK<int>();
            queue.Enqueue(1);

            Assert.That(queue.IsEmpty(), Is.EqualTo(false));
        }

        [Test]
        public void Serialization_SerializesQueueToJsonFile()
        {
            var queue = new QueueLSK<string>("apple", "banana", "cherry");
            string filePath = "queue.json";

            queue.Serialization(filePath);

            Assert.That(File.Exists(filePath), Is.EqualTo(true));

            File.Delete(filePath);
        }

        [Test]
        public void Serialization_ThrowsException_WhenFileIsNotJson()
        {
            var queue = new QueueLSK<int>(1, 2, 3);
            string filePath = "queue.txt";

            Assert.Throws<InvalidOperationException>(() => queue.Serialization(filePath), "Файл должен быть JSON.");
        }

        [Test]
        public void Deserialization_DeserializesQueueFromJsonFile()
        {
            var queue = new QueueLSK<string>("apple", "banana", "cherry");
            string filePath = "queue.json";
            queue.Serialization(filePath);

            var deserializedQueue = QueueLSK<string>.Deserialization(filePath);

            CollectionAssert.AreEqual(queue, deserializedQueue!);

            File.Delete(filePath);
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileDoesNotExist()
        {
            string filePath = "nonexistent.json";

            Assert.Throws<InvalidOperationException>(() => QueueLSK<int>.Deserialization(filePath), "Отсутствует файл по указанному пути.");
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileIsNotJson()
        {
            string filePath = "queue.txt";

            Assert.Throws<InvalidOperationException>(() => QueueLSK<int>.Deserialization(filePath), "Файл должен быть JSON.");

            File.Delete(filePath);
        }
    }
}
