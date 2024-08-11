using NUnit.Framework;
using ListStructureKit;
using NUnit.Framework.Legacy;

namespace ListStructureKitTests
{
    [TestFixture]
    public class SinglyLinkedListLSKTests
    {
        [Test]
        public void AddFirst_AddsElementToFrontOfList()
        {
            var list = new SinglyLinkedListLSK<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            Assert.That(list.Size, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
        }

        [Test]
        public void AddLast_AddsElementToEndOfList()
        {
            var list = new SinglyLinkedListLSK<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            Assert.That(list.Size, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(3));
        }

        [Test]
        public void AddBefore_AddsElementBeforeSpecifiedElement()
        {
            var list = new SinglyLinkedListLSK<int>(1, 2, 4);
            list.AddBefore(4, 3);

            Assert.That(list.Size, Is.EqualTo(4));
            Assert.That(list.First!.Next!.Next!.Value, Is.EqualTo(3));
        }

        [Test]
        public void AddAfter_AddsElementAfterSpecifiedElement()
        {
            var list = new SinglyLinkedListLSK<int>(1, 2, 4);
            list.AddAfter(2, 3);

            Assert.That(list.Size, Is.EqualTo(4));
            Assert.That(list.First!.Next!.Next!.Value, Is.EqualTo(3));
        }

        [Test]
        public void RemoveFirst_RemovesAndReturnsElementFromFrontOfList()
        {
            var list = new SinglyLinkedListLSK<char>('a', 'b', 'c');

            char removedValue = list.RemoveFirst();

            Assert.That(removedValue, Is.EqualTo('a'));
            Assert.That(list.First!.Value, Is.EqualTo('b'));
            Assert.That(list.Size, Is.EqualTo(2));
        }

        [Test]
        public void RemoveFirst_ThrowsException_WhenListIsEmpty()
        {
            var list = new SinglyLinkedListLSK<int>();

            Assert.Throws<InvalidOperationException>(() => list.RemoveFirst(), "Попытка удалить элемент из пустого списка.");
        }

        [Test]
        public void RemoveLast_RemovesAndReturnsElementFromEndOfList()
        {
            var list = new SinglyLinkedListLSK<char>('a', 'b', 'c');

            char removedValue = list.RemoveLast();

            Assert.That(removedValue, Is.EqualTo('c'));
            Assert.That(list.Last!.Value, Is.EqualTo('b'));
            Assert.That(list.Size, Is.EqualTo(2));
        }

        [Test]
        public void RemoveLast_ThrowsException_WhenListIsEmpty()
        {
            var list = new SinglyLinkedListLSK<int>();

            Assert.Throws<InvalidOperationException>(() => list.RemoveLast(), "Попытка удалить элемент из пустого списка.");
        }

        [Test]
        public void Remove_RemovesFirstOccurrenceOfElement()
        {
            var list = new SinglyLinkedListLSK<int>(1, 2, 3, 2, 4);

            list.Remove(2);

            Assert.That(list.Size, Is.EqualTo(4));
            Assert.That(list.First!.Next!.Value, Is.EqualTo(3));
        }

        [Test]
        public void Clear_EmptiesTheList()
        {
            var list = new SinglyLinkedListLSK<double>(1.1, 2.2, 3.3);

            list.Clear();

            Assert.That(list.Size, Is.EqualTo(0));
            Assert.That(list.First, Is.EqualTo(null));
            Assert.That(list.Last, Is.EqualTo(null));
        }        

        [Test]
        public void Concat_MergesTwoLists()
        {
            var list1 = new SinglyLinkedListLSK<int>(1, 2, 3);
            var list2 = new SinglyLinkedListLSK<int>(4, 5, 6);

            var concatenatedList = SinglyLinkedListLSK<int>.Concat(list1, list2);

            Assert.That(concatenatedList!.Size, Is.EqualTo(6));

            int[] expectedValues = { 1, 2, 3, 4, 5, 6 };
            int index = 0;
            foreach (var item in concatenatedList)
            {
                Assert.That(item, Is.EqualTo(expectedValues[index++]));
            }
        }

        [Test]
        public void IsEmpty_ReturnsTrue_WhenListIsEmpty()
        {
            var list = new SinglyLinkedListLSK<int>();

            Assert.That(list.IsEmpty(), Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_ReturnsFalse_WhenListIsNotEmpty()
        {
            var list = new SinglyLinkedListLSK<int>();
            list.AddLast(1);

            Assert.That(list.IsEmpty(), Is.EqualTo(false));
        }

        [Test]
        public void Serialization_SerializesListToJsonFile()
        {
            var list = new SinglyLinkedListLSK<string>("apple", "banana", "cherry");
            string filePath = "list.json";

            list.Serialization(filePath);

            Assert.That(File.Exists(filePath), Is.EqualTo(true));

            File.Delete(filePath);
        }

        [Test]
        public void Serialization_ThrowsException_WhenFileIsNotJson()
        {
            var list = new SinglyLinkedListLSK<int>(1, 2, 3);
            string filePath = "list.txt";

            Assert.Throws<InvalidOperationException>(() => list.Serialization(filePath), "Файл должен быть JSON.");
        }

        [Test]
        public void Deserialization_DeserializesListFromJsonFile()
        {
            var list = new SinglyLinkedListLSK<string>("apple", "banana", "cherry");
            string filePath = "list.json";
            list.Serialization(filePath);

            var deserializedList = SinglyLinkedListLSK<string>.Deserialization(filePath);

            CollectionAssert.AreEqual(list, deserializedList!);

            File.Delete(filePath);
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileDoesNotExist()
        {
            string filePath = "nonexistent.json";

            Assert.Throws<InvalidOperationException>(() => SinglyLinkedListLSK<int>.Deserialization(filePath), "Отсутствует файл по указанному пути.");
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileIsNotJson()
        {
            string filePath = "list.txt";

            Assert.Throws<InvalidOperationException>(() => SinglyLinkedListLSK<int>.Deserialization(filePath), "Файл должен быть JSON.");

            File.Delete(filePath);
        }
    }
}
