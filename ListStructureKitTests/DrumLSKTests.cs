using ListStructureKit;
using NUnit.Framework;

namespace ListStructureKitTests
{
    [TestFixture]
    public class DrumLSKTests
    {
        [Test]
        public void Constructor_InitializesDrumWithValidCapacity()
        {
            var drum = new DrumLSK<int>(5);

            Assert.That(drum.Capacity, Is.EqualTo(5));
        }

        [Test]
        public void Constructor_ThrowsExceptionForInvalidCapacity()
        {
            Assert.Throws<ArgumentException>(() => new DrumLSK<int>(0), "Емкость должна быть больше нуля.");
            Assert.Throws<ArgumentException>(() => new DrumLSK<int>(-1), "Емкость должна быть больше нуля.");
        }

        [Test]
        public void WriteAndRead_WritesValueToTopAndThenReadsValueFromTop()
        {
            var drum = new DrumLSK<char>(3);

            drum.Write('a');

            Assert.That(drum.Read(), Is.EqualTo('a'));
        }

        [Test]
        public void RotateClockwise_RotatesDrum()
        {
            var drum = new DrumLSK<double>(3);
            drum.Write(1.1);
            drum.RotateClockwise();
            drum.Write(2.2);
            drum.RotateClockwise();
            drum.Write(3.3);

            drum.RotateClockwise();
            Assert.That(drum.Read(), Is.EqualTo(1.1));
            drum.RotateClockwise();
            Assert.That(drum.Read(), Is.EqualTo(2.2));
            drum.RotateClockwise();
            Assert.That(drum.Read(), Is.EqualTo(3.3));
        }

        [Test]
        public void RotateCounterClockwise_RotatesDrum()
        {
            var drum = new DrumLSK<int>(3);
            drum.Write(1);
            drum.RotateClockwise();
            drum.Write(2);
            drum.RotateClockwise();
            drum.Write(3);

            drum.RotateCounterClockwise();
            Assert.That(drum.Read(), Is.EqualTo(2));
            drum.RotateCounterClockwise();
            Assert.That(drum.Read(), Is.EqualTo(1));
            drum.RotateCounterClockwise();
            Assert.That(drum.Read(), Is.EqualTo(3));
        }

        [Test]
        public void Clear_ClearsAllValuesInDrum()
        {
            var drum = new DrumLSK<int>(3);
            drum.Write(1);
            drum.RotateClockwise();
            drum.Write(2);
            drum.RotateClockwise();
            drum.Write(3);

            drum.Clear();
            for (int i = 0; i < 3; i++)
            {
                Assert.That(drum.Read(), Is.EqualTo(default(int)));
                drum.RotateClockwise();
            }
        }

        [Test]
        public void Contains_ReturnsTrueIfValueIsPresent()
        {
            var drum = new DrumLSK<int>(3);
            drum.Write(1);
            drum.RotateClockwise();
            drum.Write(2);
            drum.RotateClockwise();
            drum.Write(3);

            Assert.That(drum.Contains(2), Is.EqualTo(true));
            Assert.That(drum.Read(), Is.EqualTo(2));
            Assert.That(drum.Contains(4), Is.EqualTo(false));
        }

        [Test]
        public void Serialization_SerializesDrumToJsonFile()
        {
            var drum = new DrumLSK<int>(3);
            drum.Write(1);
            drum.RotateClockwise();
            drum.Write(2);
            drum.RotateClockwise();
            drum.Write(3);
            string filePath = "drum.json";

            drum.Serialization(filePath);

            Assert.That(File.Exists(filePath), Is.EqualTo(true));

            File.Delete(filePath);
        }

        [Test]
        public void Serialization_ThrowsException_WhenFileIsNotJson()
        {
            var drum = new DrumLSK<int>(3);
            string filePath = "drum.txt";

            Assert.Throws<InvalidOperationException>(() => drum.Serialization(filePath), "Файл должен быть JSON.");
        }

        [Test]
        public void Deserialization_DeserializesDrumFromJsonFile()
        {
            var drum = new DrumLSK<int>(3);
            drum.Write(1);
            drum.RotateClockwise();
            drum.Write(2);
            drum.RotateClockwise();
            drum.Write(3);
            drum.RotateClockwise();
            string filePath = "drum.json";
            drum.Serialization(filePath);

            var deserializedDrum = DrumLSK<int>.Deserialization(filePath);

            Assert.That(deserializedDrum!.Capacity, Is.EqualTo(drum.Capacity));
            for (int i = 0; i < drum.Capacity; i++)
            {
                Assert.That(deserializedDrum.Read(), Is.EqualTo(drum.Read()));
                deserializedDrum.RotateClockwise();
                drum.RotateClockwise();
            }

            File.Delete(filePath);
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileDoesNotExist()
        {
            string filePath = "nonexistent.json";

            Assert.Throws<InvalidOperationException>(() => DrumLSK<int>.Deserialization(filePath), "Отсутствует файл по указанному пути.");
        }

        [Test]
        public void Deserialization_ThrowsException_WhenFileIsNotJson()
        {
            string filePath = "drum.txt";

            Assert.Throws<InvalidOperationException>(() => DrumLSK<int>.Deserialization(filePath), "Файл должен быть JSON.");

            File.Delete(filePath);
        }
    }
}
